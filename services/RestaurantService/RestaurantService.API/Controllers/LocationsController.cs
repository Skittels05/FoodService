using Microsoft.AspNetCore.Mvc;
using RestaurantService.API.RequestModels;
using RestaurantService.BLL.Common;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.API.Controllers;

[ApiController]
[Route("api/locations")]
public class LocationsController(
    ILocationService locationService, 
    IMappingService mappingService) : ControllerBase
{
    [HttpGet("~/api/restaurants/{restaurantId:guid}/locations")]
    public async Task<ActionResult<PagedList<LocationDto>>> GetAllByRestaurant(
        [FromRoute] Guid restaurantId,
        [FromQuery] PageRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await locationService.GetAllByRestaurantIdAsync(restaurantId, request, cancellationToken));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LocationDto>> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        return Ok(await locationService.GetByIdAsync(id, cancellationToken));
    }

    [HttpGet("nearby")]
    public async Task<ActionResult<IEnumerable<RestaurantNearbyDto>>> GetNearby(
        [FromQuery] GetNearbyLocationsRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await locationService.GetNearbyAsync(
            request.Latitude, 
            request.Longitude, 
            request.RadiusKm, 
            cancellationToken));
    }

    [HttpPost("~/api/restaurants/{restaurantId:guid}/locations")]
    public async Task<ActionResult<Guid>> Create(
        [FromRoute] Guid restaurantId,
        [FromBody] CreateLocationRequest request,
        CancellationToken cancellationToken)
    {
        var dto = mappingService.Map<CreateLocationRequest, CreateLocationDto>(request);
        var locationId = await locationService.CreateAsync(restaurantId, dto, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = locationId }, locationId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateLocationRequest request,
        CancellationToken cancellationToken)
    {
        var dto = mappingService.Map<UpdateLocationRequest, UpdateLocationDto>(request);
        await locationService.UpdateAsync(id, dto, cancellationToken);

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
