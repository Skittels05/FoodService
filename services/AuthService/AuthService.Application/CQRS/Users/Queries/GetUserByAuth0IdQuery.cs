using AuthService.Application.DTO.Users;
using MediatR;

namespace AuthService.Application.CQRS.Users.Queries;

public record GetUserByAuth0IdQuery(string Auth0Id) : IRequest<UserAccountDto?>;
