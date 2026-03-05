using AuthService.Application.CQRS.RestaurantManagers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Extensions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Handlers;

public class VerifyRestaurantManagerCommandHandler(
    IUnitOfWork unitOfWork,
    IAuth0RoleService auth0RoleService,
    ICurrentUserService currentUserService)
    : IRequestHandler<VerifyRestaurantManagerCommand>
{
    public async Task Handle(VerifyRestaurantManagerCommand request, CancellationToken cancellationToken)
    {
        var pendingManager = await unitOfWork.RestaurantManagerRepository.GetByIdAsync(request.ManagerId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.RestaurantManager), request.ManagerId);

        currentUserService.EnsureHasAccessToRestaurant(pendingManager.ManagedRestaurantId);

        pendingManager.Verify();
        await unitOfWork.RestaurantManagerRepository.UpdateAsync(pendingManager, cancellationToken);

        var user = await unitOfWork.UserRepository.GetByIdAsync(pendingManager.UserId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.User), pendingManager.UserId);

        user.AssignRole(UserRole.RestaurantManager);
        await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

        await auth0RoleService.AssignRoleAsync(user.Auth0Id, UserRole.RestaurantManager, cancellationToken);
        await auth0RoleService.SetRestaurantIdAsync(user.Auth0Id, pendingManager.ManagedRestaurantId, cancellationToken);
        await auth0RoleService.SetAsVerifiedAsync(user.Auth0Id, cancellationToken);
    }
}
