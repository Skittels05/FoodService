using AuthService.Application.CQRS.CustomerAddresses.Commands;
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
                await unitOfWork.CustomerAddressRepository.DeleteAsync(request.Id, cancellationToken);
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
