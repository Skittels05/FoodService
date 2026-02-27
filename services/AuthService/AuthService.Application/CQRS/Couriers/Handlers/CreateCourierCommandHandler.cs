using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class CreateCourierCommandHandler(
    IUnitOfWork unitOfWork,
    IAuth0RoleService auth0RoleService,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<CreateCourierCommand, Guid>
{
    public async Task<Guid> Handle(CreateCourierCommand request, CancellationToken cancellationToken)
    {
        var auth0Id = currentUserService.Auth0Id
            ?? throw new UnauthorizedException();

        var user = await unitOfWork.UserRepository.GetByAuth0IdAsync(auth0Id, cancellationToken)
            ?? throw new NotFoundByAuth0Exception(auth0Id);

        if (user.Role is not UserRole.None)
            throw new RoleAlreadyAssignedException();

        request.UserId = user.Id;
        var courier = mapper.Map<Courier>(request);
        await unitOfWork.CourierRepository.AddAsync(courier, cancellationToken);
        user.AssignRole(UserRole.Courier);
        await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
        await auth0RoleService.AssignRoleAsync(user.Auth0Id,UserRole.Courier, cancellationToken);

        return courier.Id;
    }
}
