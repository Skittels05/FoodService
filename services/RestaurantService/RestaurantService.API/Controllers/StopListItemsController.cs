using Microsoft.AspNetCore.Mvc;
using RestaurantService.API.RequestModels;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.API.Controllers;

[ApiController]
[Route("api/stop-list-items")]
public class StopListItemsController(
    IStopListService stopListService,
    IMappingService mappingService) : ControllerBase
{
    [HttpPost("~/api/locations/{locationId:guid}/stop-list")]
    public async Task<ActionResult<Guid>> AddItem(
        [FromRoute] Guid locationId,
        [FromBody] AddStopListItemRequest request,
        CancellationToken cancellationToken)
    {
        var dto = mappingService.Map<AddStopListItemRequest, AddStopListItemDto>(request);
        var itemId = await stopListService.AddItemAsync(locationId, dto, cancellationToken);

        return Created(string.Empty, itemId);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoveItem(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await stopListService.RemoveItemAsync(id, cancellationToken);

        return NoContent();
    }
}
