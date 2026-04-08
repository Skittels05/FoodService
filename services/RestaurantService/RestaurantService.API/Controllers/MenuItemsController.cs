using Microsoft.AspNetCore.Mvc;
using RestaurantService.API.RequestModels;
using RestaurantService.BLL.Common;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.API.Controllers;

[ApiController]
[Route("api/menu-items")]
public class MenuItemsController(
    IMenuItemService menuItemService, 
    IMappingService mappingService) : ControllerBase
{
    [HttpGet("~/api/restaurants/{restaurantId:guid}/menu")]
    public async Task<ActionResult<PagedList<MenuItemDto>>> GetAllByRestaurant(
        [FromRoute] Guid restaurantId,
        [FromQuery] PageRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await menuItemService.GetAllByRestaurantIdAsync(restaurantId, request, cancellationToken));
    }

    [HttpPost("~/api/restaurants/{restaurantId:guid}/menu")]
    public async Task<ActionResult<Guid>> Create(
        [FromRoute] Guid restaurantId,
        [FromBody] CreateMenuItemRequest request,
        CancellationToken cancellationToken)
    {
        var dto = mappingService.Map<CreateMenuItemRequest, CreateMenuItemDto>(request);
        var menuItemId = await menuItemService.CreateAsync(restaurantId, dto, cancellationToken);

        return Created(string.Empty, menuItemId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateMenuItemRequest request,
        CancellationToken cancellationToken)
    {
        var dto = mappingService.Map<UpdateMenuItemRequest, UpdateMenuItemDto>(request);
        await menuItemService.UpdateAsync(id, dto, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await menuItemService.DeleteAsync(id, cancellationToken);

        return NoContent();
    }
}
