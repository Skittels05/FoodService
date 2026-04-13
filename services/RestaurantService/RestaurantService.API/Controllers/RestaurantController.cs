using Microsoft.AspNetCore.Mvc;
using RestaurantService.API.Mappers;
using RestaurantService.API.RequestModels;
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

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateRestaurantRequest request, 
        CancellationToken cancellationToken)
    {
        var dto = request.ToDto();
        var restaurantId = await restaurantService.CreateAsync(dto, cancellationToken);

        return Ok(restaurantId);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(
        [FromRoute] Guid id, 
        [FromBody] UpdateRestaurantRequest request, 
        CancellationToken cancellationToken)
    {
        var dto = request.ToDto(id);
        await restaurantService.UpdateAsync(dto, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        await restaurantService.DeleteAsync(id, cancellationToken);

        return Ok();
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<ActionResult> UpdateStatus(
        [FromRoute] Guid id, 
        [FromBody] UpdateRestaurantStatusRequest request, 
        CancellationToken cancellationToken)
    {
        var dto = request.ToDto(id);
        await restaurantService.UpdateActiveStatusAsync(dto, cancellationToken);

        return Ok();
    }

    [HttpPost("{id:guid}/verify")]
    public async Task<ActionResult> Verify(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        await restaurantService.VerifyAsync(id, cancellationToken);

        return Ok();
    }
}
