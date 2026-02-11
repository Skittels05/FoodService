using AuthService.Application.Common.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Commands;

public record MarkAddressAsUsedCommand(Guid Id) : IRequest, ITransactionalCommand;
