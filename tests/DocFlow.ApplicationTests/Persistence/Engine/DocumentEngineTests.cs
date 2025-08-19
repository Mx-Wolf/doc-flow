using DocFlow.Application.Persistence.Engine;
using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.ApplicationTests.Persistence.Engine;
public class DocumentEngineTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IDocumentRunnerFactory> _documentRunnerFactoryMock = new();
    private readonly Mock<IDocumentRunner> _documentRunnerMock = new();
    private readonly Mock<IRepository<RunSession, RunSessionId>> _repositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    private readonly DocumentEngine _engine;

    public DocumentEngineTests()
    {
        _engine = new DocumentEngine(
            _documentRunnerFactoryMock.Object,
            _repositoryMock.Object,
            _unitOfWorkMock.Object
        );
        _fixture.Register<RunSession>(() => _fixture.Create<ComputeSession>());
    }

    [Fact]
    public async Task ComputeAsync_ReturnsSuccess_WhenAllStepsSucceed()
    {
        // Arrange
        var document = new Document
        {
            Id = _fixture.Create<DocumentId>(),
            FormularId = _fixture.Create<FormularId>(),
            TrackId = _fixture.Create<TrackId>(),
            Station = _fixture.Create<Station>(),
            Created = new AtBy { At = DateTime.UtcNow, By = "user" },
            Updated = null,
            LastKnownRunSessionId = null,
        };
        var computeSession = _fixture.Create<ComputeSession>();
        var runSession = computeSession as RunSession;
        var cancellationToken = CancellationToken.None;

        _repositoryMock.Setup(r => r.Add(It.IsAny<RunSession>()))
            .Returns(Result<RunSession,Exception>.Success( runSession));
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(cancellationToken))
            .ReturnsAsync(Result<int, Exception>.Success(1));
        _documentRunnerFactoryMock.Setup(f => f.BeginComputeSession(document))
            .Returns(_documentRunnerMock.Object);
        _documentRunnerMock.Setup(r => r.RunActions(cancellationToken))
            .ReturnsAsync(Result<RunSession, Exception>.Success(runSession));
        // Act
        var result = await _engine.ComputeAsync(document, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(runSession, result.Value);
    }

    
    [Fact]
    public async Task ComputeAsync_ReturnsFailure_WhenActionLoopFails()
    {
        // Arrange
        var document = new Document
        {
            Id = _fixture.Create<DocumentId>(),
            FormularId = _fixture.Create<FormularId>(),
            TrackId = _fixture.Create<TrackId>(),
            Station = _fixture.Create<Station>(),
            Created = new AtBy { At = DateTime.UtcNow, By = "user" },
            Updated = null,
            LastKnownRunSessionId = null
        };
        var computeSession = _fixture.Create<ComputeSession>();
        var runSession = computeSession as RunSession;
        var cancellationToken = CancellationToken.None;
        var exception = new Exception("Action loop failed");


        _documentRunnerFactoryMock.Setup(f => f.BeginComputeSession(document))
            .Returns(_documentRunnerMock.Object);
        _documentRunnerMock.Setup(r => r.RunActions(cancellationToken))
            .ReturnsAsync(Result<RunSession, Exception>.Failure(exception));
        // Act

        var result = await _engine.ComputeAsync(document, cancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(exception, result.Error);
    }

    
}
