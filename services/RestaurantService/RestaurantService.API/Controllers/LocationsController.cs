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
public class LocationsController(ILocationService locationService) : ControllerBase
{
    [HttpGet("~/api/restaurants/{restaurantId:guid}/locations")]
    public async Task<ActionResult<PagedList<LocationDto>>> GetAllByRestaurant(
        [FromRoute] Guid restaurantId,
        [FromQuery] PageRequest request,
        CancellationToken cancellationToken)
    {
        var locations = await locationService.GetAllByRestaurantIdAsync(restaurantId, request, cancellationToken);
        return Ok(locations);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LocationDto>> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var location = await locationService.GetByIdAsync(id, cancellationToken);
        return Ok(location);
    }

    [HttpGet("nearby")]
    public async Task<ActionResult<IEnumerable<RestaurantNearbyDto>>> GetNearby(
        [FromQuery] GetNearbyLocationsQuery query,
        CancellationToken cancellationToken)
    {
        var dto = query.ToDto();
        var nearbyRestaurants = await locationService.GetNearbyAsync(dto, cancellationToken);
        
        return Ok(nearbyRestaurants);
    }

    [HttpPost("~/api/restaurants/{restaurantId:guid}/locations")]
    public async Task<ActionResult<Guid>> Create(
        [FromRoute] Guid restaurantId,
        [FromBody] CreateLocationRequest request,
        CancellationToken cancellationToken)
    {
        var dto = request.ToDto(restaurantId);
        var locationId = await locationService.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = locationId }, locationId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateLocationRequest request,
        CancellationToken cancellationToken)
    {
        var dto = request.ToDto(id);
        await locationService.UpdateAsync(dto, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await locationService.DeleteAsync(id, cancellationToken);

        return NoContent();
    }
}
