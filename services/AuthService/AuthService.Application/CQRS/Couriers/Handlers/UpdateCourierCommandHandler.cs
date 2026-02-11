using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class UpdateCourierCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCourierCommand>
{
    public async Task Handle(UpdateCourierCommand request, CancellationToken cancellationToken)
    {
        var courier = await unitOfWork.CourierRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Courier), request.Id);
        courier.ChangeVehicle(request.VehicleType);
        courier.ChangeName(request.Name);
        await unitOfWork.CourierRepository.UpdateAsync(courier, cancellationToken);
    }
}
