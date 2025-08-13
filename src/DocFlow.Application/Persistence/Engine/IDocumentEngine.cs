using System.Text.Json.Nodes;

using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

public interface IDocumentEngine
{
    Task<Result<Document, Exception>> CreateDocumentAsync(
        Station startupStation,
        JsonObject body,
        CancellationToken cancellationToken);   
    

    Task<Result<ComputeSession, Exception>> ComputeAsync(
        Document document,
        CancellationToken cancellationToken);
}
