using AuthService.Application.CQRS.CustomerAddresses.Queries;
using AuthService.Application.DTO.Customers;
using AuthService.Application.Exceptions;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Handlers;

public class GetAddressByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAddressByIdQuery, CustomerAddressDto?>
{
    public async Task<CustomerAddressDto?> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var address = await unitOfWork.CustomerAddressRepository.GetByIdAsync(request.Id, false, cancellationToken)
            ?? throw new NotFoundException(nameof(CustomerAddresses), request.Id);
        return mapper.Map<CustomerAddressDto>(address);
    }
}
