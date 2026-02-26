using AuthService.Application.CQRS.Users.Queries;
using AuthService.Application.DTO.Users;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Users.Handlers;

public class GetCurrentUserQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<GetCurrentUserQuery, UserAccountDto?>
{
    public async Task<UserAccountDto?> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var auth0Id = currentUserService.Auth0Id
            ?? throw new UnauthorizedException();
        var user = await unitOfWork.UserRepository.GetByAuth0IdAsync(auth0Id, cancellationToken);
        return user is null ? null : mapper.Map<UserAccountDto>(user);
    }
}
