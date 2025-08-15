using System.Text.Json.Nodes;

using DocFlow.Application.Engine.Stations.Commands;
using DocFlow.Application.Persistence.Engine;
using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;


namespace DocFlow.ApplicationTests;
    
public class StationCreateDocumentHandlerTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IRepositoryReadAccess<Station, StationId>> _stations = new();
    private readonly Mock<IDocumentFactory> _documentFactory = new();
    private readonly Mock<IRepository<Document, DocumentId>> _documents = new();
    private readonly Mock<IDocumentEngine> _documentEngine = new();

    public StationCreateDocumentHandlerTests()
    {
        _fixture.Register<RunSession>(() => _fixture.Create<ComputeSession>());
    }

   

    [Fact]
    public async Task StationCreateDocumentHandler_HandleAsync_ReturnsDocumentKeyOnSuccess()
    {
        // Arrange
        var stationId = _fixture.Create<StationId>();
        var commandBody = new JsonObject { ["field"] = "value" };
        var command = new StationCreateDocument(stationId, commandBody);

        var station = _fixture.Create<Station>();
        var documentKey = _fixture.Create<DocumentKey>();
        var computeSession = _fixture.Create<ComputeSession>();
        var document = _fixture.Create<Document>();


        _stations
            .Setup(r => r.FindAsync(stationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Station, Exception>.Success(station));


        _documentFactory
            .Setup(e => e.CreateFromJson(station, commandBody, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Document, Exception>.Success(document));

        _documents
            .Setup(r => r.Add(document))
            .Returns(Result<Document, Exception>.Success(document));

        _documentEngine
            .Setup(e => e.ComputeAsync(document, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<RunSession, Exception>.Success(computeSession));
        StationCreateDocumentHandler handler = GetSut();

        // Act
        var result = await handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value.Id, computeSession.Document.Id.Value);
    }

    private StationCreateDocumentHandler GetSut()
    {
        return new StationCreateDocumentHandler(
            _stations.Object,
            _documentFactory.Object,
            _documents.Object,
            _documentEngine.Object
        );
    }
}
