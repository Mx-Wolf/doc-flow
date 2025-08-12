using System.Text.Json.Nodes;

using DocFlow.Application.Engine.Stations.Commands;
using DocFlow.Application.Persistence.Engine;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;


namespace DocFlow.ApplicationTests;
    
public class StationCreateDocumentHandlerTests
{
    private readonly Fixture _fixture = new Fixture();
   
    public StationCreateDocumentHandlerTests()
    {
        _fixture.Register<RunSession>(() => _fixture.Create<InPlaceSession>());
    }
    [Fact]
    public async Task StationCreateDocumentHandler_HandleAsync_ReturnsDocumentKeyOnSuccess()
    {
        // Arrange
        var stationId = _fixture.Create<StationId>();
        var commandBody = new JsonObject { ["field"] = "value" };
        var command = new StationCreateDocument(stationId, commandBody);

        var stationDto = _fixture.Create<StationDto>();
        var documentKey = _fixture.Create<DocumentKey>();
        var documentDto = _fixture.Create<DocumentDto>();

        var stationsRepositoryMock = new Mock<IStationsRepository>();
        stationsRepositoryMock
            .Setup(r => r.GetStationAsync(stationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<StationDto,Exception>.Success(stationDto));

        var documentEngineMock = new Mock<IDocumentEngine>();
        documentEngineMock
            .Setup(e => e.CreateDocumentAsync(stationDto, commandBody, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<DocumentDto,Exception>.Success(documentDto));

        documentEngineMock
            .Setup(e => e.UpdateDocumentAsync(documentDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<DocumentKey,Exception>.Success(documentKey));

        var handler = new StationCreateDocumentHandler(
            stationsRepositoryMock.Object,
            documentEngineMock.Object
        );

        // Act
        var result = await handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value, documentKey);
    }
}
