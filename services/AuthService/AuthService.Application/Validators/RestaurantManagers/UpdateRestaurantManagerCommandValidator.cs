using AuthService.Application.Constants;
using AuthService.Application.CQRS.RestaurantManagers.Commands;
using AuthService.Domain.Constants;
using FluentValidation;

namespace AuthService.Application.Validators.RestaurantManagers;

public class UpdateRestaurantManagerCommandValidator : AbstractValidator<UpdateRestaurantManagerCommand>
{
    public UpdateRestaurantManagerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.ManagedRestaurantId)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.NameMaxLength).WithMessage(ValidatorMessages.MaxLength);
    }
}
