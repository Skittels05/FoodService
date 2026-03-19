using FluentValidation;
using RestaurantService.BLL.Constants;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.BLL.Validators;

public class AddStopListItemDtoValidator : AbstractValidator<AddStopListItemDto>
{
    public AddStopListItemDtoValidator()
    {
        RuleFor(x => x.MenuItemId)
            .NotEmpty()
            .WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Reason)
            .IsInEnum()
            .WithMessage(ValidatorMessages.InvalidEnum);

        RuleFor(x => x.Description)
            .MaximumLength(ValidationConstants.StopListReasonMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);
    }
}
