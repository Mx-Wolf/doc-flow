using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;

public class AmbientState
{
    public required int Id { get; init; }
    public required int FormularId { get; init; }
    public required int StationMapId { get; set; }
    public required int StationId { get; set; }
    public required int LastRunId { get; set; } 

    public required AtBy Created { get; init; }
    public required AtBy Updated { get; init; }
    
    
}