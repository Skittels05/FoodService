using AuthService.Application.CQRS.Customers.Queries;
using AuthService.Application.DTO.Customers;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Handlers
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(IGenericRepository<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(User), request.Id);

            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
