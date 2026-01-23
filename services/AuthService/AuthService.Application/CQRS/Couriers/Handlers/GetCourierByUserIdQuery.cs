using AuthService.Application.CQRS.Couriers.Queries;
using AuthService.Application.DTO.Courier;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class GetCourierByUserIdHandler(
    IGenericRepository<Courier> courierRepository,
    IMapper mapper)
    : IRequestHandler<GetCourierByUserIdQuery, CourierDto?>
{
    public async Task<CourierDto?> Handle(GetCourierByUserIdQuery request, CancellationToken cancellationToken)
    {
        var couriers = await courierRepository.FindAsync(c => c.UserId == request.UserId);
        var courier = couriers.FirstOrDefault()
            ?? throw new NotFoundByUserException(nameof(Courier), request.UserId);
        return mapper.Map<CourierDto>(courier);
    }
}
