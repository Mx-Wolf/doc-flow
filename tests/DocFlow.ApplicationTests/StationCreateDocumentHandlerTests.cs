using System.Text.Json.Nodes;

using DocFlow.Application.Engine.Stations.Commands;
using DocFlow.Application.Persistence.Engine;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;


namespace DocFlow.ApplicationTests;
    
public class StationCreateDocumentHandlerTests
{
    private readonly Fixture _fixture = new();
   
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

        var stationsRepositoryMock = new Mock<IStationsRepository>();
        stationsRepositoryMock
            .Setup(r => r.GetStationAsync(stationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Station,Exception>.Success(station));

        var documentEngineMock = new Mock<IDocumentEngine>();
        documentEngineMock
            .Setup(e => e.CreateDocumentAsync(station, commandBody, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Document,Exception>.Success(document));

        documentEngineMock
            .Setup(e => e.ComputeAsync(document, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<ComputeSession,Exception>.Success(computeSession));

        var handler = new StationCreateDocumentHandler(
            stationsRepositoryMock.Object,
            documentEngineMock.Object
        );

        // Act
        var result = await handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value.Id, computeSession.Document.Id.Value);
    }
}
