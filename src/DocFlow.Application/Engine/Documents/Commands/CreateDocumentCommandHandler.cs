using DocFlow.Domain.Values;

namespace DocFlow.Application.Engine.Documents.Commands;

public class CreateDocumentCommandHandler: CommandHandler<CreateDocumentCommand, CreateDocumentResult>
{
    public override async Task<Result<CreateDocumentResult,Exception>> HandleAsync(
        CreateDocumentCommand command, 
        CancellationToken cancellationToken)
    {
        var result = new CreateDocumentResult(12543);
        return await Task.FromResult(Success(result));

    }
}