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
    //исправь
    [HttpPost("sync")]
    public async Task<ActionResult<Guid>> Sync(CancellationToken cancellationToken)
    {
        var auth0Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value
                    ?? User.FindFirst("email")?.Value;
        var name = User.FindFirst("nickname")?.Value
                   ?? User.FindFirst("name")?.Value;

        if (string.IsNullOrEmpty(auth0Id)) return Unauthorized();

        var command = new SyncAuth0UserCommand(auth0Id, email ?? "", name ?? "");
        var userId = await mediator.Send(command, cancellationToken);

        return Ok(userId);
    }
    //исправь
    [HttpGet("me")]
    public async Task<ActionResult<UserAccountDto>> GetCurrentUserInfo(CancellationToken cancellationToken)
    {
        var auth0Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(auth0Id)) return Unauthorized();
        var user = await mediator.Send(new GetUserByAuth0IdQuery(auth0Id), cancellationToken);
        return user is not null ? Ok(user) : NotFound();
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
