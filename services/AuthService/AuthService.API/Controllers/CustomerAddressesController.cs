using AuthService.Application.CQRS.CustomerAddresses.Commands;
using AuthService.Application.CQRS.CustomerAddresses.Queries;
using AuthService.Application.DTO.Customers;
using AuthService.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomerAddressesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<PagedList<CustomerAddressDto>>> GetAll([FromQuery] GetAllAddressesQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "CustomerOrAdmin")]
    public async Task<ActionResult<CustomerAddressDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAddressByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpGet("customer/{customerId:guid}")]
    [Authorize(Policy = "CustomerOrAdmin")]
    public async Task<ActionResult<PagedList<CustomerAddressDto>>> GetByCustomer(Guid customerId, [FromQuery] GetCustomerAddressesQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query with { CustomerId = customerId }, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "CustomerOrAdmin")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateAddressCommand command, CancellationToken cancellationToken)
    {
        var addressId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = addressId }, addressId);
    }

    [HttpPost("{id:guid}/mark-used")]
    [Authorize(Policy = "CustomerOrAdmin")]
    public async Task<ActionResult> MarkAsUsed(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new MarkAddressAsUsedCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "CustomerOrAdmin")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteAddressCommand(id), cancellationToken);
        return NoContent();
    }
}
