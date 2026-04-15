using FluentValidation;
using RestaurantService.API.Constants;
using RestaurantService.BLL.Constants;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.API.Validators;

public class GetNearbyLocationsDtoValidator : AbstractValidator<GetNearbyLocationsDto>
{
    public GetNearbyLocationsDtoValidator()
    {
        RuleFor(x => x.Latitude)
            .InclusiveBetween(ValidationConstants.LatitudeMin, ValidationConstants.LatitudeMax)
            .WithMessage(ValidatorMessages.InclusiveBetween);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(ValidationConstants.LongitudeMin, ValidationConstants.LongitudeMax)
            .WithMessage(ValidatorMessages.InclusiveBetween);

        RuleFor(x => x.RadiusKm)
            .GreaterThan(ValidationConstants.RadiusMin)
            .WithMessage(ValidatorMessages.GreaterThan)
            .LessThanOrEqualTo(ValidationConstants.RadiusMax)
            .WithMessage(ValidatorMessages.LessThanOrEqualTo);
    }
}

public class CreateLocationDtoValidator : AbstractValidator<CreateLocationDto>
{
    public CreateLocationDtoValidator()
    {
        RuleFor(x => x.RestaurantId)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.LocationAddressMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);

        RuleFor(x => x.Latitude)
            .InclusiveBetween(ValidationConstants.LatitudeMin, ValidationConstants.LatitudeMax)
            .WithMessage(ValidatorMessages.InclusiveBetween);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(ValidationConstants.LongitudeMin, ValidationConstants.LongitudeMax)
            .WithMessage(ValidatorMessages.InclusiveBetween);
    }
}

public class UpdateLocationDtoValidator : AbstractValidator<UpdateLocationDto>
{
    public UpdateLocationDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.LocationAddressMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);

        RuleFor(x => x.Latitude)
            .InclusiveBetween(ValidationConstants.LatitudeMin, ValidationConstants.LatitudeMax)
            .WithMessage(ValidatorMessages.InclusiveBetween);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(ValidationConstants.LongitudeMin, ValidationConstants.LongitudeMax)
            .WithMessage(ValidatorMessages.InclusiveBetween);
    }
}
