using System.ComponentModel.DataAnnotations;

namespace DocFlow.Domain.Entities;

public abstract class StateDetails
{
    public required int AmbientStateId { get; init; }

}
public class IncidentReport: StateDetails
{
    public required DateTime ReportedAt { get; set; }
    [MaxLength(80)]
    public required string Description { get; set; }

}
