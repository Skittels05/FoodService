using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Domain.Enums;
using FluentValidation;

namespace AuthService.Application.Validators.Couriers;

public class UpdateCourierCommandValidator : AbstractValidator<UpdateCourierCommand>
{
    public UpdateCourierCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Courier ID is required.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Courier name is required.")
            .MaximumLength(50).WithMessage("Courier name must not exceed 50 characters.");
        RuleFor(x => x.VehicleType)
            .IsInEnum().WithMessage("Invalid vehicle type.")
            .NotEqual(VehicleType.None).WithMessage("Vehicle type must be selected.");
    }
}
