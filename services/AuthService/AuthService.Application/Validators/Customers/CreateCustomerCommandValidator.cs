using FluentValidation;
using AuthService.Application.CQRS.Customers.Commands;

namespace AuthService.Application.Validators.Customers;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(50).WithMessage("Customer name must not exceed 50 characters.");
    }
}
