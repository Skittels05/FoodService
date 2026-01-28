using AuthService.Application.CQRS.RestaurantManagers.Queries;
using AuthService.Application.DTO.RestaurantManagers;
using AuthService.Domain.Common;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Handlers;

public class GetAllRestaurantManagersHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllRestaurantManagersQuery, PagedList<RestaurantManagerDto>>
{
    public async Task<PagedList<RestaurantManagerDto>> Handle(GetAllRestaurantManagersQuery request, CancellationToken cancellationToken)
    {
        var pagedManagers = await unitOfWork.RestaurantManagerRepository
            .GetAllAsync(request.Page, request.PageSize, cancellationToken);

        return mapper.Map<PagedList<RestaurantManagerDto>>(pagedManagers);
    }
}
