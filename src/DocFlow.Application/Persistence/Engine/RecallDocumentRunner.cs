
using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

internal class RecallDocumentRunner(
    ISequenceSource sequenceSource,
    ISystemTime systemTime,
    IActionUser actionUser,
    IRepository<Station,StationId> stationsRepository,
    ForwardSession forwardSession) : IDocumentRunner
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
        var runSessionId = new RunSessionId(await sequenceSource.GetNextGuidAsync(cancellationToken));

        var runSession = new RecallSession(
            runSessionId,
            systemTime.UtcNow,
            await actionUser.GetUserStamp(cancellationToken),
            forwardSession);
        forwardSession.Document.Station = await FindSourceStation(cancellationToken);
        return runSession;
    }

    private async Task<Station> FindSourceStation(CancellationToken cancellationToken)
    {
        var result = await stationsRepository.FindAsync(forwardSession.SourceStationId, cancellationToken);
        return result.Match(
            station => station,
            error => throw new InvalidOperationException($"Source station not found: {forwardSession.SourceStationId}"));
    }
}