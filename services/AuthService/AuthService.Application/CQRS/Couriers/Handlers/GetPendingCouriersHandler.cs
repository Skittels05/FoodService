using AuthService.Application.CQRS.Couriers.Queries;
using AuthService.Application.DTO.Courier;
using AuthService.Domain.Common; // Подключаем PagedList
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
        var courierDtos = mapper.Map<List<CourierDto>>(pagedCouriers.Items);
        return new PagedList<CourierDto>(
            courierDtos,
            pagedCouriers.TotalCount,
            pagedCouriers.PageNumber,
            pagedCouriers.PageSize);
    }
}