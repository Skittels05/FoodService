using AuthService.Application.CQRS.CustomerAddresses.Queries;
using AuthService.Application.DTO.Customers;
using AuthService.Application.Exceptions;
using AuthService.Application.Extensions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Handlers;

public class GetAddressByIdHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetAddressByIdQuery, CustomerAddressDto?>
{
    public async Task<CustomerAddressDto?> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var address = await unitOfWork.CustomerAddressRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.CustomerAddress), request.Id);

        var currentUser = await unitOfWork.UserRepository.GetByAuth0IdAsync(currentUserService.Auth0Id!, cancellationToken)
            ?? throw new UnauthorizedException();

        currentUserService.EnsureIsOwnerOrAdmin(currentUser.Id, address.CustomerId);

        return mapper.Map<CustomerAddressDto>(address);
    }
}
