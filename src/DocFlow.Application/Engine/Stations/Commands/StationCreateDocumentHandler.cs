using System.Text.Json.Nodes;

using DocFlow.Application.Persistence.Engine;
using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Engine.Stations.Commands;

public record StationCreateDocument(StationId StationId, JsonObject Body);
public class StationCreateDocumentHandler(
    IRepositoryReadAccess<Station, StationId> stationsRepository,
    IDocumentFactory documentFactory,
    IRepository<Document, DocumentId> documentRepository,
    IDocumentEngine documentEngine
    ) : CommandHandler<StationCreateDocument, DocumentKey>
{

    public override async Task<Result<DocumentKey, Exception>> HandleAsync(StationCreateDocument command, CancellationToken cancellationToken)
    {

        return await stationsRepository.FindAsync(command.StationId, cancellationToken)
            .BindAsync(e => documentFactory.CreateFromJson(e, command.Body, cancellationToken))
            .BindAsync(documentRepository.Add)
            .BindAsync(e => documentEngine.ComputeAsync(e, cancellationToken))
            .MapAsync(e => new DocumentKey(e.Document.Id.Value));
        ;
    }

}
