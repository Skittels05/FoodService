using AuthService.Application.CQRS.Users.Commands;
using AuthService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.CQRS.Users.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly UserManager<User> _userManager;

        public DeleteUserCommandHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task Handle(DeleteUserCommand request, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return;

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                throw new Exception("Failde to deltete user");
        }
    }
}
