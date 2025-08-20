using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;

namespace DocFlow.Application.Persistence.Engine;

public class DocumentRunnerFactory(
    ISequenceSource sequenceSource,
    ISystemTime systemTime,
    IActionUser actionUser,
    IRepository<Station, StationId> stationsRepository
    ) : IDocumentRunnerFactory
{
    public IDocumentRunner BeginComputeSession(Document document) =>
        new ComputeDocumentRunner(sequenceSource, systemTime, actionUser, document);
    public IDocumentRunner BeginForwardSession(Document document, Channel channel) =>
        new ForwardDocumentRunner(sequenceSource, systemTime, actionUser, document, channel);
    public IDocumentRunner BeginRecallSession(ForwardSession forwardSession) =>
        new RecallDocumentRunner(sequenceSource, systemTime, actionUser, stationsRepository, forwardSession);
}