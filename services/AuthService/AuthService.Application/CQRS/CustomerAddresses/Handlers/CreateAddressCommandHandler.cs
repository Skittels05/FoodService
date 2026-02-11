using AuthService.Application.CQRS.CustomerAddresses.Commands;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Handlers;

public class CreateAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateAddressCommand, Guid>
{
    public async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = mapper.Map<CustomerAddress>(request);
        await unitOfWork.CustomerAddressRepository.AddAsync(address, cancellationToken);
        return address.Id;
    }
}
