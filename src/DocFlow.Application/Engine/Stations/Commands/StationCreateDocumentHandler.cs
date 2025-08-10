using System.Text.Json.Nodes;

using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Engine.Stations.Commands;

public record StationCreateDocument(StationId StationId, JsonObject Body);
public class StationCreateDocumentHandler:CommandHandler<StationCreateDocument, DocumentKey>
{
    public override Task<Result<DocumentKey,Exception>> HandleAsync(StationCreateDocument command, CancellationToken cancellationToken)
    {
        // find:
        //   1. track and formular by StationId
        //   2. map the Body to the formular data type
        //   3. create a document with the track and formular
        throw new NotImplementedException();
    }
}
