using DocFlow.Domain.Entities.StateMachine.State;

namespace DocFlow.Application.Persistence.Engine;

public static class SequenceSourceExtensions
{
    public static async Task<RunSessionId> GetRunSessionId(this ISequenceSource sequenceSource, CancellationToken cancellationToken)
        => new RunSessionId(await sequenceSource.GetNextGuidAsync(cancellationToken));
}