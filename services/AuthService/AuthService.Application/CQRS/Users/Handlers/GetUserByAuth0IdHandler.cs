using AuthService.Application.CQRS.Users.Queries;
using AuthService.Application.DTO.Users;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Users.Handlers;

public class GetUserByAuth0IdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetUserByAuth0IdQuery, UserAccountDto?>
{
    public async Task<UserAccountDto?> Handle(GetUserByAuth0IdQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByAuth0IdAsync(request.Auth0Id, cancellationToken);
        return mapper.Map<UserAccountDto>(user);
    }
}
