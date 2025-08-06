using Microsoft.AspNetCore.Mvc;

namespace DocFlow.Api.Controllers.DocFlowEngine;

[ApiController]
[Route("api/documents")]
public class Documents
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        await Task.CompletedTask;
        return new OkObjectResult(new { Message = "Hello from Documents!" });
    }

}