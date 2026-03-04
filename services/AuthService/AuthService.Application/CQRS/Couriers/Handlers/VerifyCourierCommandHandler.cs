using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class VerifyCourierCommandHandler(
    IUnitOfWork unitOfWork,
    IAuth0RoleService auth0RoleService)
    : IRequestHandler<VerifyCourierCommand>
{
    public async Task Handle(VerifyCourierCommand request, CancellationToken cancellationToken)
    {
        var courier = await unitOfWork.CourierRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Courier), request.Id);
        var user = await unitOfWork.UserRepository.GetByIdAsync(courier.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), courier.UserId);

        courier.Verify();
        await unitOfWork.CourierRepository.UpdateAsync(courier, cancellationToken);
        await auth0RoleService.SetAsVerifiedAsync(user.Auth0Id, cancellationToken);
    }
}
