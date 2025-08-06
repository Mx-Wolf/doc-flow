using DocFlow.Domain.Values;

namespace DocFlow.Application.Engine.Events;

public record DocumentEventsRequestCommand(int Id, DocumentEventType Type, string Payload);

public class DocumentEventsService : ICommandHandler<DocumentEventsRequestCommand, DocumentKey>
{
    public async Task<DocumentKey> HandleAsync(DocumentEventsRequestCommand command, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new DocumentKey(command.Id));
    }
}
