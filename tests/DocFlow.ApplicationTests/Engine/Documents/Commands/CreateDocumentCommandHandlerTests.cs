using System.Text.Json.Nodes;

using DocFlow.Application.Engine.Documents.Commands;
using DocFlow.Application.Persistence.Engine;
using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.ApplicationTests.Engine.Documents.Commands;

public class CreateDocumentCommandHandlerTests
{
    private readonly Fixture _fixture = new();
    [Fact]
    public async Task HandleAsync_ReturnsCreateDocumentResult_WhenAllDependenciesSucceed()
    {
        // Arrange
        DocumentId documentId = _fixture.Create<DocumentId>();
        FormularId formularId = _fixture.Create<FormularId>();
        StationId stationId = _fixture.Create<StationId>();
        JsonObject properties = JsonObject.Parse("{}")!.AsObject();
        Presentable presentable = _fixture.Create<Presentable>();
        CreateDocumentCommand command = new(documentId, stationId, properties);
        Track track = _fixture.Create<Track>();
        Station station = _fixture.Build<Station>()
            .With(e=>e.Id, stationId)
            .With(e=>e.Track, track)
            .Create();
        Document document = _fixture.Build<Document>()
            .With(d => d.Id, documentId)
            .With(d => d.FormularId, formularId)
            .With(d => d.Station, station)
            .Create();

        var stationsRepositoryMock = new Mock<IRepository<Station, StationId>>();
        stationsRepositoryMock
            .Setup(r => r.FindAsync(stationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Station, Exception>.Success(station));

        var documentFactoryMock = new Mock<IDocumentFactory>();
        documentFactoryMock
            .Setup(f => f.CreateFromJson(station, properties, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Document, Exception>.Success(document));
        var documentRunnerMock = new Mock<IDocumentRunner>();
        documentRunnerMock
            .Setup(r => r.RunActions(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<RunSession, Exception>.Success(_fixture.Create<ComputeSession>()));

        var documentRunnerFactoryMock = new Mock<IDocumentRunnerFactory>();
        documentRunnerFactoryMock.Setup(e=>e.BeginComputeSession(It.IsAny<Document>())).Returns(
            Result<IDocumentRunner, Exception>.Success(documentRunnerMock.Object));
        var computeSessionMock = _fixture.Create<ComputeSession>();
       
        var documentRepositoryMock = new Mock<IRepository<Document, DocumentId>>();
        documentRepositoryMock.Setup(r => r.Add(It.IsAny<Document>()))
            .Returns(Result<Document, Exception>.Success(document));

        var documentEngineMock = new Mock<IDocumentEngine>();
        documentEngineMock.Setup(e => e.ComputeAsync(document, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<RunSession, Exception>.Success(computeSessionMock));

        var handler = new CreateDocumentCommandHandler(
            documentFactoryMock.Object,
            stationsRepositoryMock.Object,
            documentRepositoryMock.Object,
            documentEngineMock.Object);

        // Act
        var result = await handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        
    }

    //[Fact]
    //public async Task HandleAsync_ReturnsException_WhenStationNotFound()
    //{
    //    // Arrange
    //    var stationId = new StationId("station-2");
    //    var properties = new Dictionary<string, object>();
    //    var command = new CreateDocumentCommand(stationId);

    //    var stationsRepositoryMock = new Mock<IRepository<Station, StationId>>();
    //    stationsRepositoryMock
    //        .Setup(r => r.FindAsync(stationId, It.IsAny<CancellationToken>()))
    //        .ReturnsAsync(Result<Station, Exception>.Failure(new KeyNotFoundException()));

    //    var documentFactoryMock = new Mock<IDocumentFactory>();
    //    var documentRunnerFactoryMock = new Mock<IDocumentRunnerFactory>();

    //    var handler = new CreateDocumentCommandHandler(
    //        documentRunnerFactoryMock.Object,
    //        documentFactoryMock.Object,
    //        stationsRepositoryMock.Object);

    //    // Act
    //    var result = await handler.HandleAsync(command, CancellationToken.None);

    //    // Assert
    //    Assert.False(result.IsSuccess);
    //    Assert.IsType<KeyNotFoundException>(result.Error);
    //}

    //[Fact]
    //public async Task HandleAsync_ReturnsException_WhenDocumentFactoryFails()
    //{
    //    // Arrange
    //    var stationId = new StationId("station-3");
    //    var properties = new Dictionary<string, object>();
    //    var command = new CreateDocumentCommand(stationId);

    //    var station = new Station(stationId, "Test Station");

    //    var stationsRepositoryMock = new Mock<IRepository<Station, StationId>>();
    //    stationsRepositoryMock
    //        .Setup(r => r.FindAsync(stationId, It.IsAny<CancellationToken>()))
    //        .ReturnsAsync(Result<Station, Exception>.Success(station));

    //    var documentFactoryMock = new Mock<IDocumentFactory>();
    //    documentFactoryMock
    //        .Setup(f => f.CreateFromJson(station, properties, It.IsAny<CancellationToken>()))
    //        .ReturnsAsync(Result<IDocumentCompute, Exception>.Failure(new InvalidOperationException()));

    //    var documentRunnerFactoryMock = new Mock<IDocumentRunnerFactory>();

    //    var handler = new CreateDocumentCommandHandler(
    //        documentRunnerFactoryMock.Object,
    //        documentFactoryMock.Object,
    //        stationsRepositoryMock.Object);

    //    // Act
    //    var result = await handler.HandleAsync(command, CancellationToken.None);

    //    // Assert
    //    Assert.False(result.IsSuccess);
    //    Assert.IsType<InvalidOperationException>(result.Error);
    //}
}