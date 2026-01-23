using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class CreateCourierCommandHandler(ICourierRepository courierRepository, IMapper mapper)
    : IRequestHandler<CreateCourierCommand, Guid>
{
    public async Task<Guid> Handle(CreateCourierCommand request, CancellationToken cancellationToken)
    {
        var courier = mapper.Map<Courier>(request);
        await courierRepository.AddAsync(courier, cancellationToken);
        return courier.Id;
    }
}
