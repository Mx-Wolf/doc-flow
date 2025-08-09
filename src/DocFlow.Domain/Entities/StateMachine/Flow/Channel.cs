using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.Flow;
[StronglyTypedId]
public partial struct ChannelId{}
public class Channel
{
    public required ChannelId Id { get; init; }
    public required StationId SourceStationId { get; init; }
    public required StationId TargetStationId { get; init; }
    public required Presentable Presentable { get; init; }

    public required Type GateKeeper { get; init; }
}