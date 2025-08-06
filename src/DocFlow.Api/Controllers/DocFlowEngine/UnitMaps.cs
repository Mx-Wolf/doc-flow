using Microsoft.AspNetCore.Mvc;

namespace DocFlow.Api.Controllers.DocFlowEngine;

[ApiController]
[Route("api/unit-maps")]
public class UnitMaps
{
    /// <summary>
    /// Creates a new document that starts its lifecycle in the specified unit map. The unit is selected by convention.
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
            new { id = 42}, new { id=42, title="yi", handlingMapId=id, activityId=id-1 });
    }
}