namespace DocFlow.Application.Engine.Documents.Commands;

public class CreateDocumentCommandHandler: ICommandHandler<CreateDocumentCommand, CreateDocumentResult>
{
    public Task<CreateDocumentResult> HandleAsync(
        CreateDocumentCommand command, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}