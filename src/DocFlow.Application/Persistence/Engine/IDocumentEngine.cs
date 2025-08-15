using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

public interface IDocumentEngine
{

    Task<Result<RunSession, Exception>> ComputeAsync(
        Document document,
        CancellationToken cancellationToken);
}

public interface IActionLoop
{
    Task<Result<RunSession, Exception>> RunActions(
        RunSession runSession,
        CancellationToken cancellationToken);
}