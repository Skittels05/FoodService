using AuthService.Application.CQRS.RestaurantManagers.Commands;
using AuthService.Application.CQRS.RestaurantManagers.Queries;
using AuthService.Application.DTO.RestaurantManagers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantManagersController(IMediator mediator, IMapper mapper) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRestaurantManagersQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetRestaurantManagerByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetManagerByUserIdQuery(userId);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("restaurant/{restaurantId:guid}")]
    public async Task<IActionResult> GetByRestaurant(Guid restaurantId, [FromQuery] GetManagersByRestaurantQuery query, CancellationToken cancellationToken)
    {

        var fullQuery = query with { RestaurantId = restaurantId };

        var result = await mediator.Send(fullQuery, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantManagerCommand command, CancellationToken cancellationToken)
    {
        var managerId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = managerId }, managerId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRestaurantManagerDto dto, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateRestaurantManagerCommand>(dto) with { Id = id };

        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteRestaurantManagerCommand(id);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
