using AuthService.Application.Common.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Commands;

public record CreateAddressCommand(Guid CustomerId, string Address) : IRequest<Guid>, ITransactionalCommand;
