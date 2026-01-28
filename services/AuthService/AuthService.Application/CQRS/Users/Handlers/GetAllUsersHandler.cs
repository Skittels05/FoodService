using AuthService.Application.CQRS.Users.Queries;
using AuthService.Application.DTO.Users;
using AuthService.Domain.Common;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Users.Handlers;

public class GetAllUsersHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllUsersQuery, PagedList<UserAccountDto>>
{
    public async Task<PagedList<UserAccountDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var pagedUsers = await unitOfWork.UserRepository
            .GetAllAsync(request.Page, request.PageSize, cancellationToken);
        return mapper.Map<PagedList<UserAccountDto>>(pagedUsers);
    }
}
