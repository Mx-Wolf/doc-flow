namespace DocFlow.Application.Engine.Documents.Commands;

public class CreateDocumentCommandHandler: ICommandHandler<CreateDocumentCommand, CreateDocumentResult>
{
    public async Task<CreateDocumentResult> HandleAsync(
        CreateDocumentCommand command, 
        CancellationToken cancellationToken)
    {
        var result = new CreateDocumentResult(12543);
        return await Task.FromResult(result);

    }
}