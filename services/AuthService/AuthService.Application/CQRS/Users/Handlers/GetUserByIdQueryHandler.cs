using AutoMapper;
using AuthService.Application.CQRS.Users.Queries;
using AuthService.Application.DTO.Users;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Users.Handlers;

public class GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetUserByIdQuery, UserAccountDto?>
{
    public async Task<UserAccountDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserManager.FindByIdAsync(request.Id.ToString());
        if (user is null) return null;
        return mapper.Map<UserAccountDto>(user);
    }
}
