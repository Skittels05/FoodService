using FluentValidation;
using RestaurantService.BLL.Constants;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.BLL.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    public CreateRestaurantDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.RestaurantNameMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);
    }
}

public class UpdateRestaurantDtoValidator : AbstractValidator<UpdateRestaurantDto>
{
    public UpdateRestaurantDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.RestaurantNameMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);
    }
}
