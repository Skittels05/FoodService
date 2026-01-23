using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class VerifyCourierCommandHandler(ICourierRepository courierRepository)
    : IRequestHandler<VerifyCourierCommand>
{
    public async Task Handle(VerifyCourierCommand request, CancellationToken cancellationToken)
    {
        var courier = await courierRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Courier), request.Id);
        courier.Verify();
        await courierRepository.UpdateAsync(courier, cancellationToken);
    }
}