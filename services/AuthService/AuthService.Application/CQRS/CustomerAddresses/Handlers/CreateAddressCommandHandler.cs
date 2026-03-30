using AuthService.Application.CQRS.CustomerAddresses.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Application.Extensions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Handlers;

public class CreateAddressCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICurrentUserService currentUserService,
    IGeoService geoService)
    : IRequestHandler<CreateAddressCommand, Guid>
{
    public async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await unitOfWork.UserRepository.GetByAuth0IdAsync(currentUserService.Auth0Id!, cancellationToken)
            ?? throw new UnauthorizedException();

        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId, cancellationToken)
            ?? throw new NotFoundException(nameof(Customer), request.CustomerId);

        currentUserService.EnsureIsOwnerOrAdmin(currentUser.Id, customer.UserId);

        var address = mapper.Map<CustomerAddress>(request);
        await unitOfWork.CustomerAddressRepository.AddAsync(address, cancellationToken);

        await geoService.AddOrUpdateLocationAsync(address.Id, address.Longitude, address.Latitude);

        return address.Id;
    }
}