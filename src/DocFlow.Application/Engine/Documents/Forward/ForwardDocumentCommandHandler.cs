using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Engine.Documents.Forward;
public class ForwardDocumentCommandHandler (
    IRepository<Document, DocumentId> documents): CommandHandler<ForwardDocumentCommand, ForwardDocumentResult>
{
    public override Task<Result<ForwardDocumentResult, Exception>> HandleAsync(ForwardDocumentCommand command, CancellationToken cancellationToken)
    {
        var x = documents.FindAsync(command.DocumentId, cancellationToken);
            
        throw new NotImplementedException();
    }
}
