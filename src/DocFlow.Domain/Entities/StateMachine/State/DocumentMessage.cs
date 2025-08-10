using System.ComponentModel.DataAnnotations;

namespace DocFlow.Domain.Entities.StateMachine.State;
[StronglyTypedId]
public readonly partial struct DocumentMessageId { }
public class DocumentMessage
{
    public required DocumentMessageId Id { get; init; }
    public required TraceRecordId TraceRecordId { get; init; }
    public required DateTime CreatedAt { get; init; }
    [MaxLength(100)]
    public required string Message { get; init; }
}