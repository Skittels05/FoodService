using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Domain.Enums;
using FluentValidation;

namespace AuthService.Application.Validators.Couriers;

public class CreateCourierCommandValidator : AbstractValidator<CreateCourierCommand>
{
    public CreateCourierCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Courier name is required.")
            .MaximumLength(50).WithMessage("Courier name must not exceed 50 characters.");
        RuleFor(x => x.VehicleType)
            .IsInEnum().WithMessage("Invalid vehicle type.")
            .NotEqual(VehicleType.None).WithMessage("Vehicle type must be selected.");
        RuleFor(x => x.DocumentsPath)
            .NotEmpty().WithMessage("Documents path is required.");
        RuleFor(x => x.PhotoVerificationPath)
            .NotEmpty().WithMessage("Photo verification path is required.");
    }
}
