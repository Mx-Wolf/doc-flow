using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;

public class ForwardSession(
    RunSessionId id, 
    DateTime generatedAt, 
    Document document, 
    AtBy created, 
    StationId sourceStationId, 
    StationId targetStationId, 
    ChannelId channelId) : RunSession(id, generatedAt, document, created, RunDirection.Forward)
{
    public StationId SourceStationId { get; init; } = sourceStationId;
    public StationId TargetStationId { get; init; } = targetStationId;
    public ChannelId ChannelId { get; init; } = channelId;
}