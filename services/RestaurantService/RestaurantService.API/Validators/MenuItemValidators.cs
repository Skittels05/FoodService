using FluentValidation;
using RestaurantService.API.Constants;
using RestaurantService.BLL.Constants;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.API.Validators;

public class CreateMenuItemDtoValidator : AbstractValidator<CreateMenuItemDto>
{
    public CreateMenuItemDtoValidator()
    {
        RuleFor(x => x.RestaurantId)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.MenuItemNameMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage(ValidatorMessages.GreaterThan);
    }
}

public class UpdateMenuItemDtoValidator : AbstractValidator<UpdateMenuItemDto>
{
    public UpdateMenuItemDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.MenuItemNameMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage(ValidatorMessages.GreaterThan);
    }
}
