using AuthService.Application.CQRS.RestaurantManagers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Handlers;

public class CreateRestaurantManagerCommandHandler(
    IUnitOfWork unitOfWork,
    IAuth0RoleService auth0RoleService,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<CreateRestaurantManagerCommand, Guid>
{
    public async Task<Guid> Handle(CreateRestaurantManagerCommand request, CancellationToken cancellationToken)
    {
        var auth0Id = currentUserService.Auth0Id
            ?? throw new UnauthorizedException();

        var user = await unitOfWork.UserRepository.GetByAuth0IdAsync(auth0Id, cancellationToken)
            ?? throw new NotFoundByAuth0Exception(auth0Id);

        if (user.Role is not UserRole.None)
            throw new RoleAlreadyAssignedException();

        request.UserId = user.Id;
        var manager = mapper.Map<Domain.Entities.RestaurantManager>(request);
        await unitOfWork.RestaurantManagerRepository.AddAsync(manager, cancellationToken);
        await auth0RoleService.AssignRoleAsync(user.Auth0Id, UserRole.RestaurantManager, cancellationToken);
        await auth0RoleService.SetRestaurantIdAsync(user.Auth0Id, request.ManagedRestaurantId, cancellationToken);
        
        return manager.Id;
    }
}
