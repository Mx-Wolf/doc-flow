using DocFlow.Application.Persistence.Engine;
using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.ApplicationTests.Persistence.Engine;
public class DocumentEngineTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IRunSessionFactory> _sessionFactoryMock = new();
    private readonly Mock<IRepository<RunSession, RunSessionId>> _sessionsMock = new();
    private readonly Mock<IActionLoop> _actionLoopMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    private readonly DocumentEngine _engine;

    public DocumentEngineTests()
    {
        _engine = new DocumentEngine(
            _sessionFactoryMock.Object,
            _sessionsMock.Object,
            _actionLoopMock.Object,
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

        _sessionFactoryMock
            .Setup(f => f.Create(document, cancellationToken))
            .ReturnsAsync(Result<RunSession, Exception>.Success(computeSession));

        _sessionsMock
            .Setup(r => r.Add(computeSession))
            .Returns(Result<RunSession, Exception>.Success(runSession));

        _actionLoopMock
            .Setup(a => a.RunActions(runSession, cancellationToken))
            .ReturnsAsync(Result<RunSession, Exception>.Success(runSession));

        _unitOfWorkMock
            .Setup(u => u.SaveChangesAsync( cancellationToken))
            .ReturnsAsync(Result<int, Exception>.Success(1));

        // Act
        var result = await _engine.ComputeAsync(document, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(runSession, result.Value);
    }

    [Fact]
    public async Task ComputeAsync_ReturnsFailure_WhenSessionFactoryFails()
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
        var cancellationToken = CancellationToken.None;
        var exception = new Exception("Factory failed");

        _sessionFactoryMock
            .Setup(f => f.Create(document, cancellationToken))
            .ReturnsAsync(Result<RunSession, Exception>.Failure(exception));

        // Act
        var result = await _engine.ComputeAsync(document, cancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(exception, result.Error);
    }

    [Fact]
    public async Task ComputeAsync_ReturnsFailure_WhenRepositoryAddFails()
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
        var cancellationToken = CancellationToken.None;
        var exception = new Exception("Add failed");

        _sessionFactoryMock
            .Setup(f => f.Create(document, cancellationToken))
            .ReturnsAsync(Result<RunSession, Exception>.Success(computeSession));

        _sessionsMock
            .Setup(r => r.Add(computeSession))
            .Returns(Result<RunSession, Exception>.Failure(exception));

        // Act
        var result = await _engine.ComputeAsync(document, cancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(exception, result.Error);
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

        _sessionFactoryMock
            .Setup(f => f.Create(document, cancellationToken))
            .ReturnsAsync(Result<RunSession, Exception>.Success(computeSession));

        _sessionsMock
            .Setup(r => r.Add(computeSession))
            .Returns(Result<RunSession, Exception>.Success(runSession));

        _actionLoopMock
            .Setup(a => a.RunActions(runSession, cancellationToken))
            .ReturnsAsync(Result<RunSession, Exception>.Failure(exception));

        // Act
        var result = await _engine.ComputeAsync(document, cancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(exception, result.Error);
    }

    [Fact]
    public async Task ComputeAsync_ReturnsFailure_WhenUnitOfWorkFails()
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
        var exception = new Exception("UnitOfWork failed");

        _sessionFactoryMock
            .Setup(f => f.Create(document, cancellationToken))
            .ReturnsAsync(Result<RunSession, Exception>.Success(computeSession));

        _sessionsMock
            .Setup(r => r.Add(computeSession))
            .Returns(Result<RunSession, Exception>.Success(runSession));

        _actionLoopMock
            .Setup(a => a.RunActions(runSession, cancellationToken))
            .ReturnsAsync(Result<RunSession, Exception>.Success(runSession));

        _unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(cancellationToken))
            .ReturnsAsync(Result<int, Exception>.Failure(exception));

        // Act
        var result = await _engine.ComputeAsync(document, cancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(exception, result.Error);
    }

    
}
