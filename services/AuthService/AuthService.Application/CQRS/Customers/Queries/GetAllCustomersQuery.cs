using AuthService.Application.DTO.Customers;
using AuthService.Domain.Common;
using AuthService.Domain.Constants;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Queries;

public record GetAllCustomersQuery : PageRequest, IRequest<PagedList<CustomerDto>>;
