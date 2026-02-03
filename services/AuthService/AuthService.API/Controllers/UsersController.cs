using AuthService.Application.CQRS.Users.Commands;
using AuthService.Application.CQRS.Users.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuthService.Application.DTO.Users;

namespace AuthService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var userId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = userId }, userId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateUserCommand>(request) with { Id = id };
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(id);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
