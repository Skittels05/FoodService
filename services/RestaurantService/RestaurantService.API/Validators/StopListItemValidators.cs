using FluentValidation;
using RestaurantService.API.Constants;
using RestaurantService.BLL.Constants;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.API.Validators;

public class AddStopListItemDtoValidator : AbstractValidator<AddStopListItemDto>
{
    public AddStopListItemDtoValidator()
    {
        RuleFor(x => x.LocationId)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.MenuItemId)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Reason)
            .IsInEnum().WithMessage(ValidatorMessages.InvalidEnum);

        RuleFor(x => x.Description)
            .MaximumLength(ValidationConstants.StopListReasonMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);
    }
}
