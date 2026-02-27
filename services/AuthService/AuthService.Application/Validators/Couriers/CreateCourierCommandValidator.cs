using AuthService.Application.Constants;
using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Domain.Constants;
using AuthService.Domain.Enums;
using FluentValidation;

namespace AuthService.Application.Validators.Couriers;

public class CreateCourierCommandValidator : AbstractValidator<CreateCourierCommand>
{
    public CreateCourierCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.NameMaxLength).WithMessage(ValidatorMessages.MaxLength);
        RuleFor(x => x.VehicleType)
            .IsInEnum().WithMessage(ValidatorMessages.InvalidEnum)
            .NotEqual(VehicleType.None).WithMessage(ValidatorMessages.MustBeSelected);
        RuleFor(x => x.DocumentsPath)
            .NotEmpty().WithMessage(ValidatorMessages.Required);
        RuleFor(x => x.PhotoVerificationPath)
            .NotEmpty().WithMessage(ValidatorMessages.Required);
    }
}
