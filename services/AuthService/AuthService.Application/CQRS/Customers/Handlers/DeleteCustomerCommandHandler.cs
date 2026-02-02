using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Handlers;

public class DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCustomerCommand>
{
    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var isDeleted = await unitOfWork.CustomerRepository.DeleteAsync(request.Id, cancellationToken);
        if (isDeleted is false)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }
    }
}
