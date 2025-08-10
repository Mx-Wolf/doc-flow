using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;
[StronglyTypedId]
public readonly partial struct DocumentId { }
public class Document
{
    public required DocumentId Id { get; init; }
    public required FormularId FormularId { get; init; }
    public required TrackId TrackId { get; set; }
    public required StationId StationId { get; set; }
    public required TraceRecordId LastTraceRecordId { get; set; } 

    public required AtBy Created { get; init; }
    public required AtBy Updated { get; init; }
    
    
}