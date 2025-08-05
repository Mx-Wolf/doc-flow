using Microsoft.AspNetCore.Mvc;

namespace DocFlow.Api.Controllers.DocFlowEngine;

[ApiController]
[Route("api/activities")]
public class Activities
{
    [HttpPost("{id:int}")]
    public async Task<IActionResult> Post(int id)
    {
        await Task.CompletedTask;

        return new CreatedAtActionResult(
            nameof(Documents.Get),
            nameof(Documents),
            new { id = 43 }, new { id = 43, title = "yi", handlingMapId = id+1, activityId=id });
    }
}