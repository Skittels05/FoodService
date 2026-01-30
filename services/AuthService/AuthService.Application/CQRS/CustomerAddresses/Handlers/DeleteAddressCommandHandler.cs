using AuthService.Application.CQRS.CustomerAddresses.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Handlers
{
    public class DeleteAddressCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAddressCommand>
    {
        public async Task Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var address = await unitOfWork.CustomerAddressRepository.GetByIdAsync(request.Id, cancellationToken)
                    ?? throw new NotFoundException(nameof(CustomerAddresses), request.Id);
                await unitOfWork.CustomerAddressRepository.DeleteAsync(address, cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch
            {
                await unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
