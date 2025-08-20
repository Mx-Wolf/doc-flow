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
    public RunSessionId Id { get; init; } = id;

    /// <summary>
    /// UTC time when the trace record was generated at the source or at the client side.
    /// </summary>
    public DateTime GeneratedAt { get; init; } = generatedAt;

    public Document Document { get; init; } = document;

    public AtBy Created { get; init; } = created;

    public RunDirection Direction { get; init; } = direction;


}