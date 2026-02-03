using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Application.CQRS.Customers.Queries;
using AuthService.Application.DTO.Customers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(IMediator mediator, IMapper mapper) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCustomersQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCustomerByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetCustomerByUserIdQuery(userId);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customerId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = customerId }, customerId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerDto request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateCustomerCommand>(request) with { Id = id };
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCustomerCommand(id);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
