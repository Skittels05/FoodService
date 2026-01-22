using AuthService.Application.CQRS.Users.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.CQRS.Users.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;

        public DeleteUserCommandHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<IdentityResult> Handle(DeleteUserCommand request, CancellationToken cancelationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            if (user is null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            return await _userManager.DeleteAsync(user);
        }
    }
}
