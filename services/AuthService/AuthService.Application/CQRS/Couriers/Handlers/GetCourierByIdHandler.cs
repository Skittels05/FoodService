using AuthService.Application.CQRS.Couriers.Queries;
using AuthService.Application.DTO.Courier;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class GetCourierByIdHandler(
    IGenericRepository<Courier> courierRepository,
    IMapper mapper)
    : IRequestHandler<GetCourierByIdQuery, CourierDto?>
{
    public async Task<CourierDto?> Handle(GetCourierByIdQuery request, CancellationToken cancellationToken)
    {
        var courier = await courierRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Courier), request.Id);
        return mapper.Map<CourierDto>(courier);
    }
}
