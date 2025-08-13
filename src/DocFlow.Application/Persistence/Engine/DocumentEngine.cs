using System.Text.Json.Nodes;

using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;
public class DocumentEngine(
    IDocumentFactory factory) : IDocumentEngine
{
    public Task<Result<ComputeSession, Exception>> ComputeAsync(Document document, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Document, Exception>> CreateDocumentAsync(Station startupStation, JsonObject body, CancellationToken cancellationToken)
    {
        try
        {
            return await factory.CreateFromJson(startupStation, body, cancellationToken);
        }
        catch (Exception ex)
        {
            return Result<Document, Exception>.Failure(ex);
        }

    }
}