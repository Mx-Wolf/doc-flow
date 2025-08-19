using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

public interface IDocumentRunner 
{
    Task<Result<RunSession, Exception>> RunActions(CancellationToken cancellationToken);
}
