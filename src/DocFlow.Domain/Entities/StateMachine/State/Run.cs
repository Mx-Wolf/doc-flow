using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;

public class Run
{
    public required int Id { get; init; }
    public required int AmbientStateId { get; init; }
    public required AtBy Created { get; init; }
    public required int SourceUnitId { get; init; }
    public required int TargetUnitId { get; init; }
    public required int LinkId { get; init; } 
    public required RunDirection Direction { get; init; }
}