using AuthService.Application.CQRS.RestaurantManagers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Handlers;

public class UpdateRestaurantManagerCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService)
    : IRequestHandler<UpdateRestaurantManagerCommand>
{
    public async Task Handle(UpdateRestaurantManagerCommand request, CancellationToken cancellationToken)
    {
        var manager = await unitOfWork.RestaurantManagerRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.RestaurantManager), request.Id);

        var managerUser = await unitOfWork.UserRepository.GetByIdAsync(manager.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.User), manager.UserId);

        if (currentUserService.Role != "Admin" && currentUserService.Auth0Id != managerUser.Auth0Id)
            throw new AccessDeniedException();

        manager.ChangeName(request.Name);
        manager.ChangeRestaurantId(request.ManagedRestaurantId);
        await unitOfWork.RestaurantManagerRepository.UpdateAsync(manager, cancellationToken);
    }
}
