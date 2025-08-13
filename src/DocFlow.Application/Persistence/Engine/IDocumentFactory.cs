using System.Text.Json.Nodes;

using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

public interface IDocumentFactory
{
    Task<Result<Document,Exception>> CreateFromJson(Station station, JsonObject rowData, CancellationToken cancellationToken);
}
