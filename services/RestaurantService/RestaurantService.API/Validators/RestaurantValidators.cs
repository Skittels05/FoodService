using FluentValidation;
using RestaurantService.API.Constants;
using RestaurantService.BLL.Constants;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.API.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    public CreateRestaurantDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.RestaurantNameMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);
    }
}

public class UpdateRestaurantDtoValidator : AbstractValidator<UpdateRestaurantDto>
{
    public UpdateRestaurantDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.RestaurantNameMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);
    }
}

public class UpdateRestaurantStatusDtoValidator : AbstractValidator<UpdateRestaurantStatusDto>
{
    public UpdateRestaurantStatusDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);
    }
}
