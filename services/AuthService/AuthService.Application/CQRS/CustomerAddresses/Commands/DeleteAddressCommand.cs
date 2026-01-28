using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Commands;

public record DeleteAddressCommand(Guid Id) : IRequest;
