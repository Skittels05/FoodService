using Microsoft.AspNetCore.Mvc;
using RestaurantService.API.RequestModels;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.API.Controllers;

[ApiController]
[Route("api/documents")]
public class RestaurantDocumentsController(
    IRestaurantDocumentService documentService,
    IMappingService mappingService) : ControllerBase
{
    [HttpPost("restaurant/{restaurantId:guid}")]
    public async Task<ActionResult<Guid>> AddDocument(
        [FromRoute] Guid restaurantId,
        [FromBody] AddRestaurantDocumentRequest request,
        CancellationToken cancellationToken)
    {
        var dto = mappingService.Map<AddRestaurantDocumentRequest, AddRestaurantDocumentDto>(request);
        var documentId = await documentService.AddDocumentAsync(restaurantId, dto, cancellationToken);

        return Created(string.Empty, documentId);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoveDocument(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await documentService.RemoveDocumentAsync(id, cancellationToken);

        return NoContent();
    }

    [HttpPut("{id:guid}/file")]
    public async Task<IActionResult> ReplaceDocument(
        [FromRoute] Guid id,
        [FromBody] ReplaceRestaurantDocumentRequest request,
        CancellationToken cancellationToken)
    {
        var dto = mappingService.Map<ReplaceRestaurantDocumentRequest, ReplaceRestaurantDocumentDto>(request);
        await documentService.ReplaceDocumentAsync(id, dto, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/approve")]
    public async Task<IActionResult> ApproveDocument(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await documentService.ApproveDocumentAsync(id, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/reject")]
    public async Task<IActionResult> RejectDocument(
        [FromRoute] Guid id,
        [FromBody] RejectRestaurantDocumentRequest request,
        CancellationToken cancellationToken)
    {
        var dto = mappingService.Map<RejectRestaurantDocumentRequest, RejectRestaurantDocumentDto>(request);
        await documentService.RejectDocumentAsync(id, dto, cancellationToken);

        return NoContent();
    }
}
