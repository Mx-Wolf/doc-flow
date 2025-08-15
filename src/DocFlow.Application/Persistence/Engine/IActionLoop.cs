using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

public interface IActionLoop
{
    Task<Result<RunSession, Exception>> RunActions(
        RunSession runSession,
        CancellationToken cancellationToken);
}

public class ActionLoop : IActionLoop
{
    public async Task<Result<RunSession, Exception>> RunActions(
        RunSession runSession,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(
            Result<RunSession, Exception>.Success(runSession)
        );
    }
}