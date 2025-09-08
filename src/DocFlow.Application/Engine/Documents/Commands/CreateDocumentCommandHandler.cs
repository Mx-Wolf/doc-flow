using DocFlow.Application.Persistence.Engine;
using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Engine.Documents.Commands;

public class CreateDocumentCommandHandler(
    IDocumentRunnerFactory documentRunnerFactory,
    IDocumentFactory documentFactory,
    IRepository<Station, StationId> stationsRepository
    ) : CommandHandler<CreateDocumentCommand, CreateDocumentResult>
{
    public override async Task<Result<CreateDocumentResult, Exception>> HandleAsync(
        CreateDocumentCommand command,
        CancellationToken cancellationToken) 
        => await (await stationsRepository.FindAsync(command.StationId, cancellationToken))
            .BindAsync(station => documentFactory.CreateFromJson(station, command.Properties, cancellationToken))
            .BindAsync(documentRunnerFactory.BeginComputeSession)
            .BindAsync(compute => compute.RunActions(cancellationToken))
            .MapAsync(session => new CreateDocumentResult(session.Id));
}