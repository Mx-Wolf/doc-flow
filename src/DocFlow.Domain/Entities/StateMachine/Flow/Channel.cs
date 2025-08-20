using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.Flow;
[StronglyTypedId]
public readonly partial struct ChannelId{}
public class Channel
{
    public required ChannelId Id { get; init; }
    public required Station SourceStation { get; init; }
    public required Station TargetStation { get; init; }
    public required Presentable Presentable { get; init; }

    public required Type GateKeeper { get; init; }
}