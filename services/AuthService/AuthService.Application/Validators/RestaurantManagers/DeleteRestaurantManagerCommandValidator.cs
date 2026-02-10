using FluentValidation;
using AuthService.Application.CQRS.RestaurantManagers.Commands;

namespace AuthService.Application.Validators.RestaurantManagers;

public class DeleteRestaurantManagerCommandValidator : AbstractValidator<DeleteRestaurantManagerCommand>
{
    public DeleteRestaurantManagerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Manager ID is required.");
    }
}
