using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Domain.Interfaces.Repositories;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Handlers;

public class DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
    : IRequestHandler<DeleteCustomerCommand>
{
    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        await customerRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
