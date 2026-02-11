using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class VerifyCourierCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<VerifyCourierCommand>
{
    public async Task Handle(VerifyCourierCommand request, CancellationToken cancellationToken)
    {
        var courier = await unitOfWork.CourierRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Courier), request.Id);
        courier.Verify();
        await unitOfWork.CourierRepository.UpdateAsync(courier, cancellationToken);
    }
}
