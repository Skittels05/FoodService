using MediatR;

namespace AuthService.Application.CQRS.Customer.Commands
{
    public record VerifyCourierCommand(Guid Id) : IRequest;
}
