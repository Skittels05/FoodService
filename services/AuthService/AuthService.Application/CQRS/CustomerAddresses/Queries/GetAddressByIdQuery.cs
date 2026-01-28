using AuthService.Application.DTO.Customers;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Queries;

public record GetAddressByIdQuery(Guid Id) : IRequest<CustomerAddressDto?>;
