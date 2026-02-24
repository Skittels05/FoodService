using AuthService.Application.Constants;
using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Domain.Constants;
using FluentValidation;

namespace AuthService.Application.Validators.Customers;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.NameMaxLength).WithMessage(ValidatorMessages.MaxLength);
    }
}
