using System.Text.Json.Nodes;

using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;

namespace DocFlow.Application.Engine.Documents.Forward;

public record ForwardDocumentCommand(
    DocumentId DocumentId,
    ChannelId ChannelId,
    JsonObject? Patch);
