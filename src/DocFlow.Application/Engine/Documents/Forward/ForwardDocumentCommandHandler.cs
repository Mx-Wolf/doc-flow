
using DocFlow.Application.Engine.Documents.Commands;
using DocFlow.Application.Persistence.Engine;
using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Engine.Documents.Forward;
public class ForwardDocumentCommandHandler (
    IDocumentFactory documentFactory,
    IRepository<Document, DocumentId> documents,
    IRepository<Channel, ChannelId> channels,
    IDocumentEngine engine,
    IUnitOfWork unitOfWork): CommandHandler<ForwardDocumentCommand, SessionResponse>
{
    public override Task<Result<SessionResponse, Exception>> HandleAsync(ForwardDocumentCommand command, CancellationToken cancellationToken)
    {
        return documents
            .FindAsync(command.DocumentId, cancellationToken)
            .BindAsync(document=>documentFactory.PatchFromJson(document,command.Patch, cancellationToken))
            .BindAsync(document => channels
              .FindAsync(command.ChannelId, cancellationToken)
              .BindAsync(channel => engine.ForwardAsync(document,channel,cancellationToken)
            ))
            .MapAsync(session => new SessionResponse(session.Id))
            .BindAsync(result => unitOfWork.SaveChangesAsync(result, cancellationToken));
    }
}
