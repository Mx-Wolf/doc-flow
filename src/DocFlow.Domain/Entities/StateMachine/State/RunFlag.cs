using System.ComponentModel.DataAnnotations;

namespace DocFlow.Domain.Entities.StateMachine.State;

public class RunFlag
{
    public required int Id { get; init; }
    public required int RunId { get; init; }

    [MaxLength(100)]
    public required string Message { get; init; }
}