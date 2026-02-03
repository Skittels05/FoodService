using AuthService.Application.DTO.Customers;
using AuthService.Domain.Common;
using AuthService.Domain.Constants;
using MediatR;

public record GetAllAddressesQuery : PageRequest, IRequest<PagedList<CustomerAddressDto>>;
