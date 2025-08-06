using Microsoft.AspNetCore.Mvc;

namespace DocFlow.Api.Controllers.DocFlowEngine;

[ApiController]
[Route("api/units")]
public class Units
{
    /// <summary>
    /// Creates a new document that starts its lifecycle in the specified unit.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/documents")]
    public async Task<IActionResult> Post(int id)
    {
        await Task.CompletedTask;

        return new CreatedAtActionResult(
            nameof(Documents.Get),
            nameof(Documents),
            new { id = 43 }, new { id = 43, title = "yi", handlingMapId = id+1, activityId=id });
    }
}