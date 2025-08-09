using DocFlow.Domain.Values;

namespace DocFlow.Application.Engine.Events;

public record DocumentEventsRequestCommand(int Id, DocumentEventType Type, string Payload);

public class DocumentEventsService : CommandHandler<DocumentEventsRequestCommand, DocumentKey>
{
    public override async Task<Result<DocumentKey, Exception>> HandleAsync(DocumentEventsRequestCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return Success(new DocumentKey(command.Id));
    }
}
