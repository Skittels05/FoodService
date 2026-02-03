using AuthService.Application.CQRS.CustomerAddresses.Commands;
using AuthService.Application.CQRS.CustomerAddresses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerAddressesController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllAddressesQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetAddressByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("customer/{customerId:guid}")]
    public async Task<IActionResult> GetByCustomer(Guid customerId, [FromQuery] GetCustomerAddressesQuery query, CancellationToken cancellationToken)
    {
        var fullQuery = query with { CustomerId = customerId };

        var result = await mediator.Send(fullQuery, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAddressCommand command, CancellationToken cancellationToken)
    {
        var addressId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = addressId }, addressId);
    }

    [HttpPost("{id:guid}/mark-used")]
    public async Task<IActionResult> MarkAsUsed(Guid id, CancellationToken cancellationToken)
    {
        var command = new MarkAddressAsUsedCommand(id);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteAddressCommand(id);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
