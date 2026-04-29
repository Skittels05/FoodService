using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantService.API.Constants;
using RestaurantService.BLL.Common;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IRestaurantService restaurantService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedList<RestaurantDto>>> GetAll(
        [FromQuery] PageRequest request, 
        CancellationToken cancellationToken)
    {
        return Ok(await restaurantService.GetAllAsync(request, cancellationToken));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RestaurantDto>> GetById(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        return Ok(await restaurantService.GetByIdAsync(id, cancellationToken));
    }

    [HttpPost("[action]")]
    [Authorize(Policy = Policies.AdminOnly)]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateRestaurantDto dto, 
        CancellationToken cancellationToken)
    {
        var restaurantId = await restaurantService.CreateAsync(dto, cancellationToken);

        return Ok(restaurantId);
    }

    [HttpPut("[action]")]
    [Authorize(Policy = Policies.ManagerOrAdmin)]
    public async Task<ActionResult> Update(
        [FromBody] UpdateRestaurantDto dto, 
        CancellationToken cancellationToken)
    {
        await restaurantService.UpdateAsync(dto, cancellationToken);

        return Ok();
    }

    [Authorize(Policy = Policies.AdminOnly)]
    [HttpPatch("[action]")]
    public async Task<ActionResult> UpdateStatus(
        [FromBody] UpdateRestaurantStatusDto dto, 
        CancellationToken cancellationToken)
    {
        await restaurantService.UpdateActiveStatusAsync(dto, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.AdminOnly)]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        await restaurantService.DeleteAsync(id, cancellationToken);

        return Ok();
    }

    [HttpPost("{id:guid}/verify")]
    [Authorize(Policy = Policies.AdminOnly)]
    public async Task<ActionResult> Verify(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        await restaurantService.VerifyAsync(id, cancellationToken);

        return Ok();
    }
}
