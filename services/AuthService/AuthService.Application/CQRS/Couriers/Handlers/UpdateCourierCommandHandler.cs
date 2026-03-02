using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class UpdateCourierCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService)
    : IRequestHandler<UpdateCourierCommand>
{
    public async Task Handle(UpdateCourierCommand request, CancellationToken cancellationToken)
    {
        var courier = await unitOfWork.CourierRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Courier), request.Id);

        var courierUser = await unitOfWork.UserRepository.GetByIdAsync(courier.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), courier.UserId);

        if (currentUserService.Role != "Admin" && currentUserService.Auth0Id != courierUser.Auth0Id)
        {
            throw new AccessDeniedException();
        }

        courier.ChangeVehicle(request.VehicleType);
        courier.ChangeName(request.Name);
        await unitOfWork.CourierRepository.UpdateAsync(courier, cancellationToken);
    }
}