using AuthService.Application.DTO.Users;
using MediatR;

namespace AuthService.Application.CQRS.Users.Queries
{
    public record GetUserByIdQuery(Guid Id) : IRequest<UserAccountDto?>;
}
