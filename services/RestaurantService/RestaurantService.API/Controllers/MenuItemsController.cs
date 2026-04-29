using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantService.API.Constants;
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
    
    [HttpPost("[action]")]
    [Authorize(Policy = Policies.ManagerOrAdmin)]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateMenuItemDto dto,
        CancellationToken cancellationToken)
    {
        var menuItemId = await menuItemService.CreateAsync(dto, cancellationToken);

        return Ok(menuItemId);
    }

    [HttpPut("[action]")]
    [Authorize(Policy = Policies.ManagerOrAdmin)]
    public async Task<ActionResult> Update(
        [FromBody] UpdateMenuItemDto dto,
        CancellationToken cancellationToken)
    {
        await menuItemService.UpdateAsync(dto, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.ManagerOrAdmin)]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await menuItemService.DeleteAsync(id, cancellationToken);

        return Ok();
    }
}
