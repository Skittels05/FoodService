using AuthService.Application.CQRS.Users.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Extensions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Users.Handlers;

public class AssignAdminRoleCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IAuth0RoleService auth0RoleService)
    : IRequestHandler<AssignAdminRoleCommand>
{
    public async Task Handle(AssignAdminRoleCommand request, CancellationToken cancellationToken)
    {
        currentUserService.EnsureIsAdmin();

        var user = await unitOfWork.UserRepository.GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.User), request.UserId);

        user.AssignRole(UserRole.Admin);
        await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

        await auth0RoleService.AssignRoleAsync(user.Auth0Id, UserRole.Admin, cancellationToken);
    }
}
