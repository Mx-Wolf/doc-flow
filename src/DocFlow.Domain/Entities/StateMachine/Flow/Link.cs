using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.Flow;

public class Link
{
    public required int Id { get; init; }
    public required int SourceStationId { get; init; }
    public required int TargetStationId { get; init; }
    public required Presentable Presentable { get; init; }

    public required Type GateKeeper { get; init; }
}