using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.Flow;
[StronglyTypedId]
public partial struct StationId { }
/// <summary>
/// Business operations are performed at the station. 
/// </summary>
public class Station
{
    public required StationId Id { get; init; }
    public required TrackId TrackId { get; init; }
    public required Presentable Presentable { get; init; }
}