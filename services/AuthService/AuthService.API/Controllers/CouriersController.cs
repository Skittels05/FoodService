using AuthService.API.Constants;
using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Application.CQRS.Couriers.Queries;
using AuthService.Application.DTO.Courier;
using AuthService.Domain.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CouriersController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [Authorize(Policy = Policies.AdminOnly)]
    [HttpGet]
    public async Task<ActionResult<PagedList<CourierDto>>> GetAll([FromQuery] GetAllCouriersQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [Authorize(Policy = Policies.AdminOnly)]
    [HttpGet("pending")]
    public async Task<ActionResult<PagedList<CourierDto>>> GetPending([FromQuery] GetPendingCouriersQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [Authorize(Policy = Policies.CourierOrAdmin)]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CourierDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCourierByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [Authorize(Policy = Policies.CourierOrAdmin)]
    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<CourierDto>> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCourierByUserIdQuery(userId), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateCourierCommand command, CancellationToken cancellationToken)
    {
        var courierId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = courierId }, courierId);
    }

    [Authorize(Policy = Policies.CourierOrAdmin)]
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateCourierCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize(Policy = Policies.AdminOnly)]
    [HttpPost("verify")]
    public async Task<ActionResult> Verify([FromBody] VerifyCourierCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize(Policy = Policies.AdminOnly)]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteCourierCommand(id), cancellationToken);
        return NoContent();
    }
}
