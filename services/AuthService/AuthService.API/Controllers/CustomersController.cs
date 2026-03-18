using AuthService.API.Constants;
using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Application.CQRS.Customers.Queries;
using AuthService.Application.DTO.Customers;
using AuthService.Domain.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomersController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [Authorize(Policy = Policies.AdminOnly)]
    [HttpGet]
    public async Task<ActionResult<PagedList<CustomerDto>>> GetAll([FromQuery] GetAllCustomersQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [Authorize(Policy = Policies.CustomerOrAdmin)]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCustomerByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [Authorize(Policy = Policies.CustomerOrAdmin)]
    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<CustomerDto>> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCustomerByUserIdQuery(userId), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customerId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = customerId }, customerId);
    }

    [Authorize(Policy = Policies.CustomerOrAdmin)]
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize(Policy = Policies.AdminOnly)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteCustomerCommand(id), cancellationToken);
        return NoContent();
    }
}
