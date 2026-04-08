using Microsoft.AspNetCore.Mvc;
using RestaurantService.API.RequestModels;
using RestaurantService.BLL.Common;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(
    IRestaurantService restaurantService,
    IMappingService mappingService) : ControllerBase
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
        var dto = mappingService.Map<CreateRestaurantRequest, CreateRestaurantDto>(request);
        var restaurantId = await restaurantService.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = restaurantId }, restaurantId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id, 
        [FromBody] UpdateRestaurantRequest request, 
        CancellationToken cancellationToken)
    {
        var dto = mappingService.Map<UpdateRestaurantRequest, UpdateRestaurantDto>(request);
        await restaurantService.UpdateAsync(id, dto, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        await restaurantService.DeleteAsync(id, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(
        [FromRoute] Guid id, 
        [FromBody] UpdateRestaurantStatusRequest request, 
        CancellationToken cancellationToken)
    {
        await restaurantService.UpdateActiveStatusAsync(id, request.IsActive, cancellationToken);

        return NoContent();
    }

    [HttpPost("{id:guid}/verify")]
    public async Task<IActionResult> Verify(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        await restaurantService.VerifyAsync(id, cancellationToken);

        return NoContent();
    }
}
