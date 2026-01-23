using AuthService.Application.CQRS.Couriers.Queries;
using AuthService.Application.DTO.Courier;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class GetPendingCouriersHandler(
    IGenericRepository<Courier> courierRepository,
    IMapper mapper)
    : IRequestHandler<GetPendingCouriersQuery, IEnumerable<CourierDto>>
{
    public async Task<IEnumerable<CourierDto>> Handle(GetPendingCouriersQuery request, CancellationToken cancellationToken)
    {
        var couriers = await courierRepository.FindAsync(c => c.IsVerified == false, cancellationToken);
        return mapper.Map<IEnumerable<CourierDto>>(couriers);
    }
}
