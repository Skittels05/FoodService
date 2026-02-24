using FluentValidation;
using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Application.Constants;

namespace AuthService.Application.Validators.Customers;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);
    }
}
