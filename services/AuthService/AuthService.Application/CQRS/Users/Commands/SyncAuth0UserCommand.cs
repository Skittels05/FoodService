using AuthService.Application.Common.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Users.Commands;

public record SyncAuth0UserCommand(
    string Auth0Id,
    string Email,
    string UserName
) : IRequest<Guid>, ITransactionalCommand;
