using Microsoft.AspNetCore.Mvc;
using RestaurantService.API.Mappers;
using RestaurantService.API.RequestModels;
using RestaurantService.BLL.Common;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.API.Controllers;

[ApiController]
[Route("api/menu-items")]
public class MenuItemsController(IMenuItemService menuItemService) : ControllerBase
{
    [HttpGet("~/api/restaurants/{restaurantId:guid}/menu")]
    public async Task<ActionResult<PagedList<MenuItemDto>>> GetAllByRestaurant(
        [FromRoute] Guid restaurantId,
        [FromQuery] PageRequest request,
        CancellationToken cancellationToken)
    {
        var menuItems = await menuItemService.GetAllByRestaurantIdAsync(restaurantId, request, cancellationToken);
        
        return Ok(menuItems);
    }

    [HttpPost("~/api/restaurants/{restaurantId:guid}/menu")]
    public async Task<ActionResult<Guid>> Create(
        [FromRoute] Guid restaurantId,
        [FromBody] CreateMenuItemRequest request,
        CancellationToken cancellationToken)
    {
        var dto = request.ToDto(restaurantId);
        var menuItemId = await menuItemService.CreateAsync(dto, cancellationToken);

        return Ok(menuItemId);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateMenuItemRequest request,
        CancellationToken cancellationToken)
    {
        var dto = request.ToDto(id);
        await menuItemService.UpdateAsync(dto, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await menuItemService.DeleteAsync(id, cancellationToken);

        return Ok();
    }
}
