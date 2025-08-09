using System.Text.Json.Nodes;

using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Engine.Stations.Commands;

public record StationCreateDocument(StationId StationId, JsonObject Body);
internal class StationCreateDocumentHandler:CommandHandler<StationCreateDocument, DocumentKey>
{
    public override Task<Result<DocumentKey,Exception>> HandleAsync(StationCreateDocument command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
