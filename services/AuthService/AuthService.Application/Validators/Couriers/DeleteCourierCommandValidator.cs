using AuthService.Application.Constants;
using AuthService.Application.CQRS.Couriers.Commands;
using FluentValidation;

namespace AuthService.Application.Validators.Couriers;

public class DeleteCourierCommandValidator : AbstractValidator<DeleteCourierCommand>
{
    public DeleteCourierCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);
    }
}
