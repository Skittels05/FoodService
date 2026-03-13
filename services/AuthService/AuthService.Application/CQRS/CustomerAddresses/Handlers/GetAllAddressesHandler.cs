using AuthService.Application.DTO.Customers;
using AuthService.Application.Extensions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Common;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Handlers;

public class GetAllAddressesHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetAllAddressesQuery, PagedList<CustomerAddressDto>>
{
    public async Task<PagedList<CustomerAddressDto>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
    {
        currentUserService.EnsureIsAdmin();

        var pagedAddresses = await unitOfWork.CustomerAddressRepository
            .GetAllAsync(request, cancellationToken);

        return mapper.Map<PagedList<CustomerAddressDto>>(pagedAddresses);
    }
}
