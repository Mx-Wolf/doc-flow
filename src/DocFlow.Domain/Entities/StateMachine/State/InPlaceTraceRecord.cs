using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;

public class InPlaceTraceRecord(
    TraceRecordId id, 
    DateTime generatedAt, 
    DocumentId ambientStateId,
    AtBy created, 
    RunDirection direction) 
    : TraceRecord(id, generatedAt, ambientStateId, created, direction)
{
    
}