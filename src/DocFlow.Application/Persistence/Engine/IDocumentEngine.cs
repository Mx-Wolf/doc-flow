using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

public interface IDocumentEngine
{

    Task<Result<RunSession, Exception>> ComputeAsync(
        Document document,
        CancellationToken cancellationToken);
}
