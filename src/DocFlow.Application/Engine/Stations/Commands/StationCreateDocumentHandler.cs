using System.Text.Json.Nodes;

using DocFlow.Application.Persistence.Engine;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Engine.Stations.Commands;

public record StationCreateDocument(StationId StationId, JsonObject Body);
public class StationCreateDocumentHandler(
    IStationsRepository stationsRepository,
    IDocumentEngine documentEngine
    ) :CommandHandler<StationCreateDocument, DocumentKey>
{
   
    public override async Task<Result<DocumentKey,Exception>> HandleAsync(StationCreateDocument command, CancellationToken cancellationToken)
    {
        
        return await stationsRepository.GetStationAsync(command.StationId, cancellationToken)
            .BindAsync(e => documentEngine.CreateDocumentAsync(e, command.Body, cancellationToken));
            ;
    }

}
