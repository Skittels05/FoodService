using AuthService.Application.Constants;
using AuthService.Application.CQRS.Users.Commands;
using FluentValidation;

namespace AuthService.Application.Validators.Users;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);
    }
}
