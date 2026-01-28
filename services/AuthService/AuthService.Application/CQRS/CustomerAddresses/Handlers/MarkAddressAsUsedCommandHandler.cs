using AuthService.Application.CQRS.CustomerAddresses.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Handlers;

public class MarkAddressAsUsedCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<MarkAddressAsUsedCommand>
{
    public async Task Handle(MarkAddressAsUsedCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var address = await unitOfWork.CustomerAddressRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(CustomerAddress), request.Id);

            address.MarkAsUsed();

            await unitOfWork.CustomerAddressRepository.UpdateAsync(address, cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
