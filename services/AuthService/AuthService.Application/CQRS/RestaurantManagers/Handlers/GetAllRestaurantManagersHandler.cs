using AuthService.Application.CQRS.RestaurantManagers.Queries;
using AuthService.Application.DTO.RestaurantManagers;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Common;
using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Handlers;

public class GetAllRestaurantManagersHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetAllRestaurantManagersQuery, PagedList<RestaurantManagerDto>>
{
    public async Task<PagedList<RestaurantManagerDto>> Handle(GetAllRestaurantManagersQuery request, CancellationToken cancellationToken)
    {
        if (currentUserService.Role != UserRole.Admin)
        {
            throw new AccessDeniedException();
        }

        var pagedManagers = await unitOfWork.RestaurantManagerRepository
            .GetAllAsync(request, cancellationToken);

        return mapper.Map<PagedList<RestaurantManagerDto>>(pagedManagers);
    }
}
