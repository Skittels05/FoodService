using FluentValidation;
using RestaurantService.BLL.Constants;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.BLL.Validators;

public class CreateMenuItemDtoValidator : AbstractValidator<CreateMenuItemDto>
{
    public CreateMenuItemDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.MenuItemNameMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage(ValidatorMessages.GreaterThan);
    }
}

public class UpdateMenuItemDtoValidator : AbstractValidator<UpdateMenuItemDto>
{
    public UpdateMenuItemDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.MenuItemNameMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage(ValidatorMessages.GreaterThan);
    }
}
