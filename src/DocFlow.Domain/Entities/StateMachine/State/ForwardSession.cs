using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;

public class ForwardSession(
    RunSessionId id, 
    DateTime generatedAt, 
    Document document, 
    AtBy created, 
    RunDirection direction,
    StationId sourceStationId, 
    StationId targetStationId, 
    ChannelId channelId) : RunSession(id, generatedAt, document, created, direction)
{
    public required StationId SourceStationId { get; init; } = sourceStationId;
    public required StationId TargetStationId { get; init; } = targetStationId;
    public required ChannelId ChannelId { get; init; } = channelId;
}