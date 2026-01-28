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
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var address = mapper.Map<CustomerAddress>(request);
            await unitOfWork.CustomerAddressRepository.AddAsync(address, cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            return address.Id;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
