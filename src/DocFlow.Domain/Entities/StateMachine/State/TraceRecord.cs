using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;
[StronglyTypedId(Template.Guid)]
public readonly partial struct TraceRecordId { }
public abstract class TraceRecord(
    TraceRecordId id,
    DateTime generatedAt,
    DocumentId ambientStateId,
    AtBy created,
    RunDirection direction)
{
    /// <summary>
    /// client side generated
    /// </summary>
    public required TraceRecordId Id { get; init; } = id;

    /// <summary>
    /// UTC time when the trace record was generated at the source or at the client side.
    /// </summary>
    public required DateTime GeneratedAt { get; init; } = generatedAt;

    public required DocumentId AmbientStateId { get; init; } = ambientStateId;
    public required AtBy Created { get; init; } = created;

    public required RunDirection Direction { get; init; } = direction;

}