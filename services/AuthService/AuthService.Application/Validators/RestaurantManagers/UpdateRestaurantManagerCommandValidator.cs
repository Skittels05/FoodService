using FluentValidation;
using AuthService.Application.CQRS.RestaurantManagers.Commands;

namespace AuthService.Application.Validators.RestaurantManagers;

public class UpdateRestaurantManagerCommandValidator : AbstractValidator<UpdateRestaurantManagerCommand>
{
    public UpdateRestaurantManagerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Manager ID is required.");

        RuleFor(x => x.ManagedRestaurantId)
            .NotEmpty().WithMessage("Managed Restaurant ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Manager name is required.")
            .MaximumLength(50).WithMessage("Manager name must not exceed 50 characters.");
    }
}
