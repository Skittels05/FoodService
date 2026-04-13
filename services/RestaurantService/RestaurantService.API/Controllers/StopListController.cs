using Microsoft.AspNetCore.Mvc;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.API.Controllers;

[ApiController]
[Route("api/stop-list")]
public class StopListController(IStopListService stopListService) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<ActionResult<Guid>> AddItem(
        [FromBody] AddStopListItemDto dto,
        CancellationToken cancellationToken)
    {
        var itemId = await stopListService.AddItemAsync(dto, cancellationToken);

        return Ok(itemId);
    }
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> RemoveItem(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await stopListService.RemoveItemAsync(id, cancellationToken);

        return Ok();
    }
}
