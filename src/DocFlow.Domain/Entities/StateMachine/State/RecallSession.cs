using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;

public class RecallSession(
    RunSessionId id,
    DateTime generatedAt,
    Document document,
    AtBy created,
    RunDirection direction,
    ForwardSession rolledBackForwardTraceRecord) : RunSession(id, generatedAt, document, created, direction)
{
    public required ForwardSession RolledBackForwardTraceRecord { get; init; } = rolledBackForwardTraceRecord;
}