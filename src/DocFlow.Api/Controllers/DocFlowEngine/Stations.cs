using System.Text.Json.Nodes;

using DocFlow.Application;

using Microsoft.AspNetCore.Mvc;

namespace DocFlow.Api.Controllers.DocFlowEngine;

[ApiController]
[Route("api/stations")]
public class Stations
{
    /// <summary>
    /// Creates a new document that starts its lifecycle in the specified unit.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/documents")]
    public async Task<IActionResult> Post(
        [FromRoute] int id,
        [FromBody] JsonObject body)
    {
        await Task.CompletedTask;

        return new CreatedAtActionResult(
            nameof(Documents.Get),
            nameof(Documents),
            new { id = id+43 }, body);
    }
}