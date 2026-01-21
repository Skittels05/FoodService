using AutoMapper;
using AuthService.Application.CQRS.Users.Queries;
using AuthService.Application.DTO.Users;
using AuthService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.CQRS.Users.Handlers;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserAccountDto?>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserAccountDto?> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user == null) return null;
        return _mapper.Map<UserAccountDto>(user);
    }
}