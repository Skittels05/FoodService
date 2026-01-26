using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Handlers;

public class DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCustomerCommand>
{
    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await unitOfWork.CustomerRepository.DeleteAsync(request.Id, cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
