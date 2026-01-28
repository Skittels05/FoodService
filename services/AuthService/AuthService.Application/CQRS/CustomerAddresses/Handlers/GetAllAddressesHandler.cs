using AuthService.Application.CQRS.CustomerAddresses.Queries;
using AuthService.Application.DTO.Customers;
using AuthService.Domain.Common;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Handlers;

public class GetAllAddressesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllAddressesQuery, PagedList<CustomerAddressDto>>
{
    public async Task<PagedList<CustomerAddressDto>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
    {
        var pagedAddresses = await unitOfWork.CustomerAddressRepository
            .GetAllAsync(request.Page, request.PageSize, cancellationToken);

        return mapper.Map<PagedList<CustomerAddressDto>>(pagedAddresses);
    }
}
