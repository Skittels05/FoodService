using FluentValidation;
using AuthService.Application.CQRS.Customers.Commands;

namespace AuthService.Application.Validators.Customers;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(50).WithMessage("Customer name must not exceed 50 characters.");
    }
}
