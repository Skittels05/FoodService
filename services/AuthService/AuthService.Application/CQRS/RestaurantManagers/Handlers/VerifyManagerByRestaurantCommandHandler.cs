using AuthService.Application.CQRS.RestaurantManagers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces;
using AuthService.Domain.Entities;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Handlers;

public class VerifyManagerByRestaurantCommandHandler(
    IUnitOfWork unitOfWork,
    IAuth0RoleService auth0RoleService) 
    : IRequestHandler<VerifyManagerByRestaurantCommand>
{
    public async Task Handle(VerifyManagerByRestaurantCommand request, CancellationToken cancellationToken)
    {

        var pendingManager = await unitOfWork.RestaurantManagerRepository.GetPendingByRestaurantIdAsync(request.RestaurantId, cancellationToken)
                             ?? throw new NotFoundException(nameof(Domain.Entities.RestaurantManager), request.RestaurantId);

        pendingManager.Verify();
        await unitOfWork.RestaurantManagerRepository.UpdateAsync(pendingManager, cancellationToken);

        var user = await unitOfWork.UserRepository.GetByIdAsync(pendingManager.UserId, cancellationToken)
                   ?? throw new NotFoundException(nameof(User), pendingManager.UserId);

        user.AssignRole(UserRole.RestaurantManager);
        await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

        await auth0RoleService.AssignRoleAsync(user.Auth0Id, UserRole.RestaurantManager, cancellationToken);
        await auth0RoleService.SetRestaurantIdAsync(user.Auth0Id, pendingManager.ManagedRestaurantId, cancellationToken);
        await auth0RoleService.SetAsVerifiedAsync(user.Auth0Id, cancellationToken);
    }
}
