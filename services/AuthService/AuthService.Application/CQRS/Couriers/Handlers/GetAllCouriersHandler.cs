using AuthService.Application.CQRS.Couriers.Queries;
using AuthService.Application.DTO.Courier;
using AuthService.Domain.Common;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class GetAllCouriersHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllCouriersQuery, PagedList<CourierDto>>
{
    public async Task<PagedList<CourierDto>> Handle(GetAllCouriersQuery request, CancellationToken cancellationToken)
    {
        var pagedCouriers = await unitOfWork.CourierRepository
            .GetAllAsync(request.Page, request.PageSize, cancellationToken);
        return mapper.Map<PagedList<CourierDto>>(pagedCouriers);
    }
}
