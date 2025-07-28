using Microsoft.AspNetCore.Mvc;

namespace DocFlow.Api.Controllers.DocFlowEngine;
[ApiController]
[Route("api/[controller]")]
public class EngineController
{
    public async Task<IActionResult> Get()
    {
        await Task.CompletedTask;
        return new OkObjectResult(new { Message = "Hello from EngineController!" });
    }
}
