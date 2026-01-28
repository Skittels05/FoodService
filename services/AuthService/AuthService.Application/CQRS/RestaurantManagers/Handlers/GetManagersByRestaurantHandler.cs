using AuthService.Application.CQRS.RestaurantManagers.Queries;
using AuthService.Application.DTO.RestaurantManagers;
using AuthService.Domain.Common;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManager.Handlers;

public class GetManagersByRestaurantHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetManagersByRestaurantQuery, PagedList<RestaurantManagerDto>>
{
    public async Task<PagedList<RestaurantManagerDto>> Handle(GetManagersByRestaurantQuery request, CancellationToken cancellationToken)
    {
        var pagedManagers = await unitOfWork.RestaurantManagerRepository
            .GetByRestaurantIdAsync(request.RestaurantId, request.Page, request.PageSize, cancellationToken);

        return mapper.Map<PagedList<RestaurantManagerDto>>(pagedManagers);
    }
}
