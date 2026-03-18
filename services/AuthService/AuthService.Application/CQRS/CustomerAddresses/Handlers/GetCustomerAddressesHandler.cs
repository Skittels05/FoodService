using AuthService.Application.CQRS.CustomerAddresses.Queries;
using AuthService.Application.DTO.Customers;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Application.Extensions;
using AuthService.Domain.Common;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Handlers;

public class GetCustomerAddressesHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetCustomerAddressesQuery, PagedList<CustomerAddressDto>>
{
    public async Task<PagedList<CustomerAddressDto>> Handle(GetCustomerAddressesQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await unitOfWork.UserRepository.GetByAuth0IdAsync(currentUserService.Auth0Id!, cancellationToken)
            ?? throw new UnauthorizedException();
        currentUserService.EnsureIsOwnerOrAdmin(currentUser.Id, request.CustomerId);

        var pagedAddresses = await unitOfWork.CustomerAddressRepository
            .GetByCustomerIdAsync(request.CustomerId, request, cancellationToken);

        return mapper.Map<PagedList<CustomerAddressDto>>(pagedAddresses);
    }
}
