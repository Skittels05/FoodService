using AuthService.Application.DTO.Customers;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Queries;

public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerDto?>;
