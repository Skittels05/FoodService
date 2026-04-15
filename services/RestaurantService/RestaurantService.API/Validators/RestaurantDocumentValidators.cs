using FluentValidation;
using RestaurantService.API.Constants;
using RestaurantService.BLL.Constants;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.API.Validators;

public class AddRestaurantDocumentDtoValidator : AbstractValidator<AddRestaurantDocumentDto>
{
    public AddRestaurantDocumentDtoValidator()
    {
        RuleFor(x => x.RestaurantId)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage(ValidatorMessages.InvalidEnum);

        RuleFor(x => x.FileUrl)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.DocumentFileUrlMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);
    }
}

public class ReplaceRestaurantDocumentDtoValidator : AbstractValidator<ReplaceRestaurantDocumentDto>
{
    public ReplaceRestaurantDocumentDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.NewFileUrl)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.DocumentFileUrlMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);
    }
}

public class RejectRestaurantDocumentDtoValidator : AbstractValidator<RejectRestaurantDocumentDto>
{
    public RejectRestaurantDocumentDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.DocumentRejectionReasonMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);
    }
}
