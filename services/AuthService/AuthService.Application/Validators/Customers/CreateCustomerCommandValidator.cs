using AuthService.Application.Constants;
using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Domain.Constants;
using FluentValidation;

namespace AuthService.Application.Validators.Customers;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.NameMaxLength).WithMessage(ValidatorMessages.MaxLength);
    }
}
