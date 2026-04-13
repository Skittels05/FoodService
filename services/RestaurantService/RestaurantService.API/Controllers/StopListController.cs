using Microsoft.AspNetCore.Mvc;
using RestaurantService.API.Mappers;
using RestaurantService.API.RequestModels;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.API.Controllers;

[ApiController]
public class StopListController(IStopListService stopListService) : ControllerBase
{
    [HttpPost("api/locations/{locationId:guid}/stop-list")]
    public async Task<ActionResult<Guid>> AddItem(
        [FromRoute] Guid locationId,
        [FromBody] AddStopListItemRequest request,
        CancellationToken cancellationToken)
    {
        var dto = request.ToDto(locationId);
        var itemId = await stopListService.AddItemAsync(dto, cancellationToken);

        return Ok(itemId);
    }

    [HttpDelete("api/stop-list/{id:guid}")]
    public async Task<ActionResult> RemoveItem(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await stopListService.RemoveItemAsync(id, cancellationToken);

        return Ok();
    }
}
