using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;

public class RecallTraceRecord(
    TraceRecordId id,
    DateTime generatedAt,
    DocumentId ambientStateId,
    AtBy created,
    RunDirection direction,
    ForwardTraceRecord rolledBackForwardTraceRecord) : TraceRecord(id, generatedAt, ambientStateId, created, direction)
{
    public required ForwardTraceRecord RolledBackForwardTraceRecord { get; init; } = rolledBackForwardTraceRecord;
}