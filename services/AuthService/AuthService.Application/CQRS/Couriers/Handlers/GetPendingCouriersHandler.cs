using AuthService.Application.CQRS.Couriers.Queries;
using AuthService.Application.DTO.Courier;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class GetPendingCouriersHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetPendingCouriersQuery, IEnumerable<CourierDto>>
{
    public async Task<IEnumerable<CourierDto>> Handle(GetPendingCouriersQuery request, CancellationToken cancellationToken)
    {
        var couriers = await unitOfWork.CourierRepository.GetPendingCouriersAsync(cancellationToken);
        return mapper.Map<IEnumerable<CourierDto>>(couriers);
    }
}
