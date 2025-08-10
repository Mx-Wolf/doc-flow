using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.Flow;
[StronglyTypedId]
public readonly partial struct FormularId { }
public class Formular
{
    public required FormularId Id { get; init; }

    public required Type DocumentData { get; set; }

    public required Presentable Presentable { get; init; }
}
