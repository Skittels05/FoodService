using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Handlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly IGenericRepository<Customer> _customerRepository;

        public DeleteCustomerCommandHandler(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerRepository.DeleteAsync(request.Id);
        }
    }
}
