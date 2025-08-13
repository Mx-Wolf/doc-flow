using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;

public class ComputeSession(
    RunSessionId id, 
    DateTime generatedAt, 
    Document document,
    AtBy created, 
    RunDirection direction) 
    : RunSession(id, generatedAt, document, created, direction)
{
    
}