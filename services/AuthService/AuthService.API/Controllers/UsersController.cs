using AuthService.Application.CQRS.Users.Commands;
using AuthService.Application.CQRS.Users.Queries;
using AuthService.Application.DTO.Users;
using AuthService.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost("sync")]
    public async Task<ActionResult<Guid>> Sync(CancellationToken cancellationToken)
    {

        var userId = await mediator.Send(new SyncAuth0UserCommand(), cancellationToken);
        return Ok(userId);
    }

    [HttpGet("me")]
    public async Task<ActionResult<UserAccountDto>> GetCurrentUserInfo(CancellationToken cancellationToken)
    {
        var user = await mediator.Send(new GetCurrentUserQuery(), cancellationToken);
        return Ok(user);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PagedList<UserAccountDto>>> GetAll([FromQuery] GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UserAccountDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Update([FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteUserCommand(id), cancellationToken);
        return NoContent();
    }
}
