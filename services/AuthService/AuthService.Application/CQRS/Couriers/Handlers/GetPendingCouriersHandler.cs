using AuthService.Application.CQRS.Couriers.Queries;
using AuthService.Application.DTO.Courier;
using AuthService.Domain.Common;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class GetPendingCouriersHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetPendingCouriersQuery, PagedList<CourierDto>>
{
    public async Task<PagedList<CourierDto>> Handle(GetPendingCouriersQuery request, CancellationToken cancellationToken)
    {
        var pagedCouriers = await unitOfWork.CourierRepository
            .GetPendingCouriersAsync(request.Page, request.PageSize, cancellationToken);
        return mapper.Map<PagedList<CourierDto>>(pagedCouriers);
    }
}
