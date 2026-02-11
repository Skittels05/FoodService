using FluentValidation;
using AuthService.Application.CQRS.Couriers.Commands;

namespace AuthService.Application.Validators.Couriers;

public class VerifyCourierCommandValidator : AbstractValidator<VerifyCourierCommand>
{
    public VerifyCourierCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Courier ID is required.");
    }
}
