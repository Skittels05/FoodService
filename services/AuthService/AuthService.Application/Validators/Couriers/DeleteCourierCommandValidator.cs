using FluentValidation;
using AuthService.Application.CQRS.Couriers.Commands;

namespace AuthService.Application.Validators.Couriers;

public class DeleteCourierCommandValidator : AbstractValidator<DeleteCourierCommand>
{
    public DeleteCourierCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Courier ID is required.");
    }
}
