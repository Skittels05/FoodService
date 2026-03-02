using AuthService.Application.CQRS.RestaurantManagers.Queries;
using AuthService.Application.DTO.RestaurantManagers;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManager.Handlers;

public class GetRestaurantManagerByIdHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetRestaurantManagerByIdQuery, RestaurantManagerDto?>
{
    public async Task<RestaurantManagerDto?> Handle(GetRestaurantManagerByIdQuery request, CancellationToken cancellationToken)
    {
        var manager = await unitOfWork.RestaurantManagerRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.RestaurantManager), request.Id);

        var managerUser = await unitOfWork.UserRepository.GetByIdAsync(manager.UserId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.User), manager.UserId);

        if (currentUserService.Role != "Admin" && currentUserService.Auth0Id != managerUser.Auth0Id)
            throw new AccessDeniedException();

        return mapper.Map<RestaurantManagerDto>(manager);
    }
}