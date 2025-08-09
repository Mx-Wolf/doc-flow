using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;
[StronglyTypedId]
public partial struct TraceRecordId { }
public class TraceRecord
{
    public required TraceRecordId Id { get; init; }
    public required DocumentId AmbientStateId { get; init; }
    public required AtBy Created { get; init; }
    public required StationId SourceStationId { get; init; }
    public required StationId TargetStationId { get; init; }
    public required ChannelId ChannelId { get; init; } 
    public required RunDirection Direction { get; init; }
}