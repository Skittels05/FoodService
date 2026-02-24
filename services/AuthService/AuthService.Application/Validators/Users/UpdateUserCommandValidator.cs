using AuthService.Application.Constants;
using AuthService.Application.CQRS.Users.Commands;
using AuthService.Domain.Constants;
using FluentValidation;

namespace AuthService.Application.Validators.Users;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.EmailMaxLength).WithMessage(ValidatorMessages.MaxLength)
            .EmailAddress().WithMessage(ValidatorMessages.InvalidEmail);

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.UserNameMaxLength).WithMessage(ValidatorMessages.MaxLength);
    }
}
