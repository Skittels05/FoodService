using AuthService.Application.CQRS.Users.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Users.Handlers;

public class SyncAuth0UserCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService)
    : IRequestHandler<SyncAuth0UserCommand, Guid>
{
    public async Task<Guid> Handle(SyncAuth0UserCommand request, CancellationToken cancellationToken)
    {
        var auth0Id = currentUserService.Auth0Id
            ?? throw new UnauthorizedException();
        var email = currentUserService.Email;
        var userName = currentUserService.Username;
        var existingUser = await unitOfWork.UserRepository
            .GetByAuth0IdAsync(auth0Id, cancellationToken);

        if (existingUser is not null)
        {
            return existingUser.Id;
        }

        var user = new User(auth0Id, email, userName, UserRole.None);
        await unitOfWork.UserRepository.AddAsync(user, cancellationToken);

        return user.Id;
    }
}
