using AuthService.Application.CQRS.RestaurantManagers.Commands;
using AuthService.Application.CQRS.RestaurantManagers.Queries;
using AuthService.Application.DTO.RestaurantManagers;
using AuthService.Domain.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RestaurantManagersController(IMediator mediator, IMapper mapper) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<PagedList<RestaurantManagerDto>>> GetAll([FromQuery] GetAllRestaurantManagersQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RestaurantManagerDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetRestaurantManagerByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<RestaurantManagerDto>> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetManagerByUserIdQuery(userId), cancellationToken);
        return Ok(result);
    }

    [HttpGet("restaurant/{restaurantId:guid}")]
    public async Task<ActionResult<PagedList<RestaurantManagerDto>>> GetByRestaurant(Guid restaurantId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetManagersByRestaurantQuery(restaurantId), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateRestaurantManagerCommand command, CancellationToken cancellationToken)
    {
        var managerId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = managerId }, managerId);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateRestaurantManagerCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteRestaurantManagerCommand(id), cancellationToken);
        return NoContent();
    }
}
