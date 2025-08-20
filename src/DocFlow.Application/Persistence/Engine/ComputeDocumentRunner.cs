
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

internal class ComputeDocumentRunner(
    ISequenceSource sequenceSource,
    ISystemTime systemTime,
    IActionUser actionUser,
    Document document) : IDocumentRunner
{
    public async Task<Result<RunSession, Exception>> RunActions(CancellationToken cancellationToken)
    {
        try
        {
            return Result<RunSession, Exception>.Success(await PrepareSession(cancellationToken));
        }
        catch (Exception ex)
        {
            return Result<RunSession, Exception>.Failure(ex);
        }
    }

    private async Task<ComputeSession> PrepareSession(CancellationToken cancellationToken)
    {
        var runSessionId = new RunSessionId(await sequenceSource.GetNextGuidAsync(cancellationToken));
        var runSession = new ComputeSession(
            runSessionId,
            systemTime.UtcNow,
            document,
            await actionUser.GetUserStamp(cancellationToken)
            );
        return runSession;
    }
}