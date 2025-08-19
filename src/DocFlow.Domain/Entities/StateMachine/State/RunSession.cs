using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;
[StronglyTypedId(Template.Guid)]
public readonly partial struct RunSessionId { }

public abstract class RunSession
    (
    RunSessionId id,
    DateTime generatedAt,
    Document document,
    AtBy created,
    RunDirection direction)
{
    /// <summary>
    /// client side generated
    /// </summary>
    public required RunSessionId Id { get; init; } = id;

    /// <summary>
    /// UTC time when the trace record was generated at the source or at the client side.
    /// </summary>
    public required DateTime GeneratedAt { get; init; } = generatedAt;

    public required Document Document { get; init; } = document;

    public required AtBy Created { get; init; } = created;

    public required RunDirection Direction { get; init; } = direction;


}