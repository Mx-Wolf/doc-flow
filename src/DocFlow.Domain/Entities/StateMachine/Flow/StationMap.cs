using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.Flow;

public class StationMap
{
    public required int Id { get; init; }
    public required int FormularId { get; init; }
    public required Presentable Presentable { get; init; }
}