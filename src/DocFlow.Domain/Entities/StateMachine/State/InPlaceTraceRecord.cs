using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;

public class InPlaceTraceRecord(
    RunSessionId id, 
    DateTime generatedAt, 
    DocumentId ambientStateId,
    AtBy created, 
    RunDirection direction) 
    : RunSession(id, generatedAt, ambientStateId, created, direction)
{
    
}