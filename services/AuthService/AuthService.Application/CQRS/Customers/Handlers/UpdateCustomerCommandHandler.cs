using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Handlers;

public class UpdateCustomerCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCustomerCommand, Guid>
{
    public async Task<Guid> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var customer = await unitOfWork.CustomerRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Customer), request.Id);
            customer.ChangeName(request.Name);
            var updatedCustomer = await unitOfWork.CustomerRepository.UpdateAsync(customer, cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            return updatedCustomer.Id;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
