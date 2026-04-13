using Microsoft.AspNetCore.Mvc;
using RestaurantService.API.Mappers;
using RestaurantService.API.RequestModels;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.API.Controllers;

[ApiController]
public class RestaurantDocumentsController(IRestaurantDocumentService documentService) : ControllerBase
{
    [HttpPost("api/restaurants/{restaurantId:guid}/documents")]
    public async Task<ActionResult<Guid>> AddDocument(
        [FromRoute] Guid restaurantId,
        [FromBody] AddRestaurantDocumentRequest request,
        CancellationToken cancellationToken)
    {
        var dto = request.ToDto(restaurantId);
        var documentId = await documentService.AddDocumentAsync(dto, cancellationToken);

        return Created(string.Empty, documentId);
    }

    [HttpDelete("api/documents/{id:guid}")]
    public async Task<ActionResult> RemoveDocument(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await documentService.RemoveDocumentAsync(id, cancellationToken);

        return NoContent();
    }

    [HttpPut("api/documents/{id:guid}")]
    public async Task<ActionResult> ReplaceDocument(
        [FromRoute] Guid id,
        [FromBody] ReplaceRestaurantDocumentRequest request,
        CancellationToken cancellationToken)
    {
        var dto = request.ToDto(id);
        await documentService.ReplaceDocumentAsync(dto, cancellationToken);

        return NoContent();
    }

    [HttpPost("api/documents/{id:guid}/approve")]
    public async Task<ActionResult> ApproveDocument(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await documentService.ApproveDocumentAsync(id, cancellationToken);

        return NoContent();
    }

    [HttpPost("api/documents/{id:guid}/reject")]
    public async Task<ActionResult> RejectDocument(
        [FromRoute] Guid id,
        [FromBody] RejectRestaurantDocumentRequest request,
        CancellationToken cancellationToken)
    {
        var dto = request.ToDto(id);
        await documentService.RejectDocumentAsync(dto, cancellationToken);

        return NoContent();
    }
}
