using Microsoft.AspNetCore.Mvc;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.API.Controllers;

[ApiController]
[Route("api/restaurant-documents")]
public class RestaurantDocumentsController(IRestaurantDocumentService documentService) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<ActionResult<Guid>> AddDocument(
        [FromBody] AddRestaurantDocumentDto dto,
        CancellationToken cancellationToken)
    {
        var documentId = await documentService.AddDocumentAsync(dto, cancellationToken);

        return Ok(documentId);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> RemoveDocument(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await documentService.RemoveDocumentAsync(id, cancellationToken);

        return Ok();
    }

    [HttpPut("[action]")]
    public async Task<ActionResult> ReplaceDocument(
        [FromBody] ReplaceRestaurantDocumentDto dto,
        CancellationToken cancellationToken)
    {
        await documentService.ReplaceDocumentAsync(dto, cancellationToken);

        return Ok();
    }

    [HttpPost("{id:guid}/approve")]
    public async Task<ActionResult> ApproveDocument(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await documentService.ApproveDocumentAsync(id, cancellationToken);

        return Ok();
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult> RejectDocument(
        [FromBody] RejectRestaurantDocumentDto dto,
        CancellationToken cancellationToken)
    {
        await documentService.RejectDocumentAsync(dto, cancellationToken);

        return Ok();
    }
}
