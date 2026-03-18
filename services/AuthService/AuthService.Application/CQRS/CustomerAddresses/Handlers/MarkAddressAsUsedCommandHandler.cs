using AuthService.Application.CQRS.CustomerAddresses.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Extensions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Handlers;

public class MarkAddressAsUsedCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService)
    : IRequestHandler<MarkAddressAsUsedCommand>
{
    public async Task Handle(MarkAddressAsUsedCommand request, CancellationToken cancellationToken)
    {
        var address = await unitOfWork.CustomerAddressRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(CustomerAddress), request.Id);

        var currentUser = await unitOfWork.UserRepository.GetByAuth0IdAsync(currentUserService.Auth0Id!, cancellationToken)
            ?? throw new UnauthorizedException();

        currentUserService.EnsureIsOwnerOrAdmin(currentUser.Id, address.CustomerId);

        address.MarkAsUsed();
        await unitOfWork.CustomerAddressRepository.UpdateAsync(address, cancellationToken);
    }
}
