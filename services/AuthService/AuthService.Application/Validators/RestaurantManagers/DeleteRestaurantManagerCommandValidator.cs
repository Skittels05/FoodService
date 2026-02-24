using AuthService.Application.Constants;
using AuthService.Application.CQRS.RestaurantManagers.Commands;
using FluentValidation;

namespace AuthService.Application.Validators.RestaurantManagers;

public class DeleteRestaurantManagerCommandValidator : AbstractValidator<DeleteRestaurantManagerCommand>
{
    public DeleteRestaurantManagerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);
    }
}
