using FluentValidation;
using AuthService.Application.CQRS.Users.Commands;

namespace AuthService.Application.Validators.Users;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User ID is required.");
    }
}
