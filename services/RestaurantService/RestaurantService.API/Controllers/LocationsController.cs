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
        [FromQuery] GetNearbyLocationsDto dto,
        CancellationToken cancellationToken)
    {
        var nearbyRestaurants = await locationService.GetNearbyAsync(dto, cancellationToken);
        
        return Ok(nearbyRestaurants);
    }
    
    [HttpPost("[action]")]
    [Authorize(Policy = Policies.ManagerOrAdmin)]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateLocationDto dto,
        CancellationToken cancellationToken)
    {
        var locationId = await locationService.CreateAsync(dto, cancellationToken);

        return Ok(locationId);
    }

    [HttpPut("[action]")]
    [Authorize(Policy = Policies.ManagerOrAdmin)]
    public async Task<ActionResult> Update(
        [FromBody] UpdateLocationDto dto,
        CancellationToken cancellationToken)
    {
        await locationService.UpdateAsync(dto, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Policies.ManagerOrAdmin)]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await locationService.DeleteAsync(id, cancellationToken);

        return Ok();
    }
}
