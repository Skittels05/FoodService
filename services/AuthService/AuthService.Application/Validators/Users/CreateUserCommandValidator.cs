using AuthService.Application.Constants;
using AuthService.Application.CQRS.Users.Commands;
using AuthService.Domain.Constants;
using AuthService.Domain.Enums;
using FluentValidation;

namespace AuthService.Application.Validators.Users;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Auth0Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.EmailMaxLength).WithMessage(ValidatorMessages.MaxLength)
            .EmailAddress().WithMessage(ValidatorMessages.InvalidEmail);

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.UserNameMaxLength).WithMessage(ValidatorMessages.MaxLength);

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage(ValidatorMessages.InvalidEnum)
            .NotEqual(UserRole.None).WithMessage(ValidatorMessages.MustBeSelected);
    }
}
