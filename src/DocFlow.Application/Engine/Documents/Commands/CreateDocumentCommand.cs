using System.Text.Json.Nodes;

using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;

namespace DocFlow.Application.Engine.Documents.Commands;
public record CreateDocumentCommand(
    DocumentId DocumentId,
    StationId StationId,
    JsonObject Properties);