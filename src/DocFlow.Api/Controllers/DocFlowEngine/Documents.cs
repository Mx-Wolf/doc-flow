using DocFlow.Application;
using DocFlow.Application.Engine.Documents.Commands;

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
    [HttpPost]
    public async Task<IActionResult> Post(
        [FromServices] ICommandHandler<CreateDocumentCommand, CreateDocumentResult> handler,
        [FromBody] CreateDocumentCommand command,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(command, cancellationToken);
        return new CreatedAtActionResult(
            nameof(Get),
            nameof(Documents),
            new { result.Id },
            result);
    }
}