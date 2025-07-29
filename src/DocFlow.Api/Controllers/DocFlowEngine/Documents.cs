using DocFlow.Application;
using DocFlow.Application.Engine.Documents.Commands;

using Microsoft.AspNetCore.Mvc;

namespace DocFlow.Api.Controllers.DocFlowEngine;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController
{
    [HttpGet]
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
            nameof(DocumentsController),
            new { result.Id },
            result);
    }
}