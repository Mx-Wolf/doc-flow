using DocFlow.Application;
using DocFlow.Application.Engine.Events;

using Microsoft.AspNetCore.Mvc;
using DocFlow.Domain.Values;

namespace DocFlow.Api.Controllers.DocFlowEngine;
public record DocumentEventRequest(DocumentEventType Type, string Payload);
[ApiController]
[Route("api/documents/{id:int}/events")]
public class DocumentEvents
{
    /// <summary>
    /// informs the document engine about an event that occurred in the document lifecycle. it initiates the processing of action in the current unit
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(
        [FromRoute] int id,
        [FromBody] DocumentEventRequest request,
        [FromServices] ICommandHandler<DocumentEventsRequestCommand, DocumentKey> service,
        CancellationToken cancellationToken)
    {
       
        var result = await service.HandleAsync(
            new DocumentEventsRequestCommand(id, request.Type, request.Payload),
            cancellationToken);
        return new OkObjectResult(result);
    }
}