using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.Flow;
[StronglyTypedId]
public partial struct TrackId { }
public class Track
{
    public required TrackId Id { get; init; }
    public required FormularId FormularId { get; init; }
    public required Presentable Presentable { get; init; }
}