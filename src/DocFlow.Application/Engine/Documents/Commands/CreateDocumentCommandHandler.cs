using DocFlow.Application.Persistence.Engine;
using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Engine.Documents.Commands;

public class CreateDocumentCommandHandler(
    IDocumentFactory documentFactory,
    IRepository<Station, StationId> stationsRepository,
    IRepository<Document, DocumentId> documentRepository,
    IDocumentEngine engine,
    IUnitOfWork unitOfWork
    ) : CommandHandler<CreateDocumentCommand, SessionResponse>
{
    public override Task<Result<SessionResponse, Exception>> HandleAsync(
        CreateDocumentCommand command,
        CancellationToken cancellationToken)
        =>  stationsRepository.FindAsync(command.StationId, cancellationToken)
        .BindAsync(station => documentFactory.CreateFromJson(station, command.Properties, cancellationToken))
        .BindAsync(documentRepository.Add)
        .BindAsync(document => engine.ComputeAsync(document, cancellationToken))
        .MapAsync(session => new SessionResponse(session.Id))
        .BindAsync(result=> unitOfWork.SaveChangesAsync(result, cancellationToken));
}