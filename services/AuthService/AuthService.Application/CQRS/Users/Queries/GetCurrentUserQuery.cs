using AuthService.Application.DTO.Users;
using MediatR;

namespace AuthService.Application.CQRS.Users.Queries;

public record GetCurrentUserQuery() : IRequest<UserAccountDto?>;
