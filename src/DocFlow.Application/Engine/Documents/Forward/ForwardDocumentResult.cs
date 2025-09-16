using DocFlow.Domain.Entities.StateMachine.State;

namespace DocFlow.Application.Engine.Documents.Forward;

public record ForwardDocumentResult(
    RunSessionId SessionId);
