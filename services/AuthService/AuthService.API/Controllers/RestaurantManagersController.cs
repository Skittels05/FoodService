using AuthService.API.Constants;
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
    [Authorize(Policy = Policies.AdminOnly)]
    [HttpGet]
    public async Task<ActionResult<PagedList<RestaurantManagerDto>>> GetAll([FromQuery] GetAllRestaurantManagersQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [Authorize(Policy = Policies.RestaurantManagerOrAdmin)]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RestaurantManagerDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetRestaurantManagerByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [Authorize(Policy = Policies.RestaurantManagerOrAdmin)]
    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<RestaurantManagerDto>> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetManagerByUserIdQuery(userId), cancellationToken);
        return Ok(result);
    }

    [Authorize(Policy = Policies.RestaurantManagerOrAdmin)]
    [HttpGet("restaurant/{restaurantId:guid}")]
    public async Task<ActionResult<PagedList<RestaurantManagerDto>>> GetByRestaurant(
    Guid restaurantId,
    [FromQuery] GetManagersByRestaurantQuery query,
    CancellationToken cancellationToken)
    {
        query.RestaurantId = restaurantId;
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateRestaurantManagerCommand command, CancellationToken cancellationToken)
    {
        var managerId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = managerId }, managerId);
    }

    [Authorize(Policy = Policies.RestaurantManagerOrAdmin)]
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateRestaurantManagerCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize(Policy = Policies.RestaurantManagerOrAdmin)]
    [HttpPost("{id:guid}/verify")]
    public async Task<IActionResult> Verify(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new VerifyRestaurantManagerCommand(id), cancellationToken);
        return NoContent();
    }

    [Authorize(Policy = Policies.AdminOnly)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteRestaurantManagerCommand(id), cancellationToken);
        return NoContent();
    }
}
