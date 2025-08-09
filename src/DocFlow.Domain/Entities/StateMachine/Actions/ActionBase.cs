namespace DocFlow.Domain.Entities.StateMachine.Actions;

[StronglyTypedId]
public partial struct ActionId { }
public class ActionBase
{
    public required ActionId Id { get; init; }
}
