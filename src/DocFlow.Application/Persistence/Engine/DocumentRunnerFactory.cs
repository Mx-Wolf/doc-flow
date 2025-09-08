using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

public class DocumentRunnerFactory(
    ISequenceSource sequenceSource,
    ISystemTime systemTime,
    IActionUser actionUser,
    IRepository<Station, StationId> stationsRepository
    ) : IDocumentRunnerFactory
{
    public Result<IDocumentRunner,Exception> BeginComputeSession(Document document) =>
        Result < IDocumentRunner,Exception>.Success( new ComputeDocumentRunner(sequenceSource, systemTime, actionUser, document));
    public Result<IDocumentRunner, Exception> BeginForwardSession(Document document, Channel channel) =>
        Result<IDocumentRunner, Exception>.Success(new ForwardDocumentRunner(sequenceSource, systemTime, actionUser, document, channel));
    public Result<IDocumentRunner, Exception> BeginRecallSession(ForwardSession forwardSession) =>
        Result<IDocumentRunner, Exception>.Success(new RecallDocumentRunner(sequenceSource, systemTime, actionUser, stationsRepository, forwardSession));
}