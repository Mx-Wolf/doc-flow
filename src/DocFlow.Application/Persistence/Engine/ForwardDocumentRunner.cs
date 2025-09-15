
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

internal class ForwardDocumentRunner(
    ISequenceSource sequenceSource,
    ISystemTime systemTime,
    IActionUser actionUser,
    Document document, Channel channel) : IDocumentRunner
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

    private async Task<RunSession> PrepareSession(CancellationToken cancellationToken)
    {
        document.Station = channel.TargetStation;

        var session = new ForwardSession(
            await sequenceSource.GetRunSessionId(cancellationToken),
            systemTime.UtcNow,
            document,
            await actionUser.GetUserStamp(cancellationToken),
            channel.SourceStation.Id,
            channel.TargetStation.Id,
            channel.Id
        );

        return session;
    }
}