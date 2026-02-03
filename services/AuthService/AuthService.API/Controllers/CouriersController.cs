using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Application.CQRS.Couriers.Queries;
using AuthService.Application.DTO.Courier;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CouriersController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCouriersQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPending([FromQuery] GetPendingCouriersQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCourierByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetCourierByUserIdQuery(userId);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCourierCommand command, CancellationToken cancellationToken)
    {
        var courierId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = courierId }, courierId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourierDto dto, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateCourierCommand>(dto) with { Id = id };
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/verify")]
    public async Task<IActionResult> Verify(Guid id, CancellationToken cancellationToken)
    {
        var command = new VerifyCourierCommand(id);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCourierCommand(id);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
