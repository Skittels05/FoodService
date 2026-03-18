using AuthService.Application.CQRS.CustomerAddresses.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Application.Extensions;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Handlers;

public class DeleteAddressCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService)
    : IRequestHandler<DeleteAddressCommand>
{
    public async Task Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await unitOfWork.CustomerAddressRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(CustomerAddress), request.Id);

        var currentUser = await unitOfWork.UserRepository.GetByAuth0IdAsync(currentUserService.Auth0Id!, cancellationToken)
            ?? throw new UnauthorizedException();

        currentUserService.EnsureIsOwnerOrAdmin(currentUser.Id, address.CustomerId);

        await unitOfWork.CustomerAddressRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
