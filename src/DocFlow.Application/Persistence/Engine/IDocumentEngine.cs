using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

public interface IDocumentEngine
{

    Task<Result<RunSession, Exception>> ComputeAsync(
        Document document,
        CancellationToken cancellationToken);

    Task<Result<RunSession,Exception>> ForwardAsync(
        Document document,
        Channel channel,
        CancellationToken cancellationToken);

    Task<Result<RunSession, Exception>> RecallAsync(
        ForwardSession forwardSession,
        CancellationToken cancellationToken);
}
