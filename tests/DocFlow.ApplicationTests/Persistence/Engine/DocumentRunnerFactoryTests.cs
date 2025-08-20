using DocFlow.Application.Persistence.Engine;
using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;

namespace DocFlow.ApplicationTests.Persistence.Engine;
public class DocumentRunnerFactoryTests
{
    private readonly Mock<ISequenceSource> _sequenceSource = new();
    private readonly Mock<ISystemTime> _systemTime = new();
    private readonly Mock<IActionUser> _actionUser = new();
    private readonly Mock<IRepository<Station, StationId>> _stationsRepository = new();
    private readonly Fixture _fixture = new();

    private DocumentRunnerFactory CreateFactory() =>
        new(_sequenceSource.Object, _systemTime.Object, _actionUser.Object, _stationsRepository.Object);

    [Fact]
    public void BeginComputeSession_Returns_ComputeDocumentRunner()
    {
        // Arrange
        var document = new Mock<Document>().Object;
        var factory = CreateFactory();

        // Act
        var runner = factory.BeginComputeSession(document);

        // Assert
        Assert.NotNull(runner);
    }

    [Fact]
    public void BeginForwardSession_Returns_ForwardDocumentRunner()
    {
        // Arrange
        var document = new Mock<Document>().Object;
        var channel = new Mock<Channel>().Object;
        var factory = CreateFactory();

        // Act
        var runner = factory.BeginForwardSession(document, channel);

        // Assert
        Assert.NotNull(runner);
    }

    [Fact]
    public void BeginRecallSession_Returns_RecallDocumentRunner()
    {
        // Arrange
        var forwardSession = _fixture.Create<ForwardSession>();
        var factory = CreateFactory();

        // Act
        var runner = factory.BeginRecallSession(forwardSession);

        // Assert
        Assert.NotNull(runner);
    }
}
