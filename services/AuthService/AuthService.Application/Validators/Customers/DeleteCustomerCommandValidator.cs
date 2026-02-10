using FluentValidation;
using AuthService.Application.CQRS.Customers.Commands;

namespace AuthService.Application.Validators.Customers;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Customer ID is required.");
    }
}
