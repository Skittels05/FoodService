using AuthService.Application.CQRS.Users.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.CQRS.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;

        public UpdateUserCommandHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<IdentityResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            if (user is null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            user.Email = request.Email;
            user.UserName = request.UserName;
            user.PhoneNumber = request.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            return result;
        }
    }
}
