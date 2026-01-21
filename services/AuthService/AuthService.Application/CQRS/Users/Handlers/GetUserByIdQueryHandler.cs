using AuthService.Application.CQRS.Users.Queries;
using AuthService.Application.DTO.Users;
using AuthService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.CQRS.Users.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserAccountDto?>
    {
        private readonly UserManager<User> _userManager;

        public GetUserByIdHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserAccountDto?> Handle(GetUserByIdQuery request, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return null;
            return new UserAccountDto(
                user.Id,
                user.Email,
                user.UserName,
                user.Role,
                user.PhoneNumber,
                user.PhoneNumberConfirmed,
                user.TwoFactorEnabled
            );
        }
    }
}
