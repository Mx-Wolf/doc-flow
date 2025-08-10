using System.Text.Json.Nodes;

using DocFlow.Application;
using DocFlow.Application.Engine.Stations.Commands;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Values;

using Microsoft.AspNetCore.Mvc;

namespace DocFlow.Api.Controllers.DocFlowEngine;

[ApiController]
[Route("api/stations")]
public class Stations
{
    /// <summary>
    /// Creates a new document that starts its lifecycle in the specified unit.
    /// </summary>
    /// <param name="stationId"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    [HttpPost("{stationId:int}/documents")]
    public async Task<IActionResult> Post(
        [FromRoute] int stationId,
        [FromBody] JsonObject body,
        [FromServices] ICommandHandler<StationCreateDocument, DocumentKey> handler,
        CancellationToken cancellationToken)
    {
        await handler.HandleAsync(
            new StationCreateDocument(new StationId(stationId), body)
            , cancellationToken
        );

        return new CreatedAtActionResult(
            nameof(Documents.Get),
            nameof(Documents),
            new { id = stationId + 43 }, body);
    }
}