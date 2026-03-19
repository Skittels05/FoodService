using FluentValidation;
using RestaurantService.BLL.Constants;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.BLL.Validators;

public class CreateLocationDtoValidator : AbstractValidator<CreateLocationDto>
{
    public CreateLocationDtoValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.LocationAddressMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);

        RuleFor(x => x.Latitude)
            .InclusiveBetween(ValidationConstants.MinLatitude, ValidationConstants.MaxLatitude)
            .WithMessage(ValidatorMessages.InvalidRange);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(ValidationConstants.MinLongitude, ValidationConstants.MaxLongitude)
            .WithMessage(ValidatorMessages.InvalidRange);
    }
}

public class UpdateLocationDtoValidator : AbstractValidator<UpdateLocationDto>
{
    public UpdateLocationDtoValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.LocationAddressMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);

        RuleFor(x => x.Latitude)
            .InclusiveBetween(ValidationConstants.MinLatitude, ValidationConstants.MaxLatitude)
            .WithMessage(ValidatorMessages.InvalidRange);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(ValidationConstants.MinLongitude, ValidationConstants.MaxLongitude)
            .WithMessage(ValidatorMessages.InvalidRange);
    }
}
