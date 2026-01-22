using AuthService.Application.CQRS.Users.Commands;
using AuthService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.CQRS.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly UserManager<User> _userManager;

        public CreateUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancelationToken)
        {
            var user = new User(request.Email, request.UserName, request.Role);
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result is { Succeeded: false })
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user: {errors}");
            }

            return user.Id;
        }
    }
}
