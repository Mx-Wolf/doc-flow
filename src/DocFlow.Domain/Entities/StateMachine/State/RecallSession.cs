using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;

public class RecallSession(
    RunSessionId id,
    DateTime generatedAt,
    AtBy created,
    ForwardSession rolledBackForwardTraceRecord) : RunSession(id, generatedAt, rolledBackForwardTraceRecord.Document, created, RunDirection.Recall)
{
    public ForwardSession RolledBackForwardTraceRecord { get; init; } = rolledBackForwardTraceRecord;
}