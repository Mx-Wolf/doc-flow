using Microsoft.AspNetCore.Mvc;

namespace DocFlow.Api.Controllers.DocFlowEngine;

[ApiController]
[Route("api/handling-maps")]
public class HandlingMap
{
    [HttpPost("{id:int}")]
    public async Task<IActionResult> Post(int id)
    {
        await Task.CompletedTask;
        
        return new CreatedAtActionResult(
            nameof(Documents.Get),
            nameof(Documents), 
            new { id = 42}, new { id=42, title="yi", handlingMapId=id, activityId=id-1 });
    }
}