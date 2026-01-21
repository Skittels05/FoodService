using AuthService.Application.CQRS.Users.Commands;
using AuthService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.CQRS.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly UserManager<User> _userManager;

        public UpdateUserCommandHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task Handle(UpdateUserCommand request, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) throw new KeyNotFoundException("User not found");

            user.Email = request.Email;
            user.UserName = request.UserName;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new Exception("Data update error");
        }
    }
}
