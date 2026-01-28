using AuthService.Application.CQRS.CustomerAddresses.Queries;
using AuthService.Application.DTO.Customers;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Handlers;

public class GetAddressByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAddressByIdQuery, CustomerAddressDto?>
{
    public async Task<CustomerAddressDto?> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var address = await unitOfWork.CustomerAddressRepository.GetByIdAsync(request.Id, cancellationToken);
        return address is null ? null : mapper.Map<CustomerAddressDto>(address);
    }
}
