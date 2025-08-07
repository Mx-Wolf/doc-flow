using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.Flow;

public class Station
{
    public required int Id { get; init; }
    public required int StationMapId { get; init; }
    public required Presentable Presentable { get; init; }
}