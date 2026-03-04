using AuthService.Application.CQRS.RestaurantManagers.Queries;
using AuthService.Application.DTO.RestaurantManagers;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Common;
using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManager.Handlers;

public class GetManagersByRestaurantHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetManagersByRestaurantQuery, PagedList<RestaurantManagerDto>>
{
    public async Task<PagedList<RestaurantManagerDto>> Handle(GetManagersByRestaurantQuery request, CancellationToken cancellationToken)
    {

        if (currentUserService.Role != UserRole.Admin)
        {
            if (currentUserService.RestaurantId != request.RestaurantId)
            {
                throw new AccessDeniedException();
            }
        }

        var pagedManagers = await unitOfWork.RestaurantManagerRepository
            .GetByRestaurantIdAsync(request.RestaurantId, request.IsVerified, request, cancellationToken);

        return mapper.Map<PagedList<RestaurantManagerDto>>(pagedManagers);
    }
}
