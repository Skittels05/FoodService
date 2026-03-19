using FluentValidation;
using RestaurantService.BLL.Constants;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.BLL.Validators;

public class AddRestaurantDocumentDtoValidator : AbstractValidator<AddRestaurantDocumentDto>
{
    public AddRestaurantDocumentDtoValidator()
    {
        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage(ValidatorMessages.InvalidEnum);

        RuleFor(x => x.FileUrl)
            .NotEmpty()
            .WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.DocumentFileUrlMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);
    }
}

public class ReplaceRestaurantDocumentDtoValidator : AbstractValidator<ReplaceRestaurantDocumentDto>
{
    public ReplaceRestaurantDocumentDtoValidator()
    {
        RuleFor(x => x.NewFileUrl)
            .NotEmpty()
            .WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.DocumentFileUrlMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);
    }
}

public class RejectRestaurantDocumentDtoValidator : AbstractValidator<RejectRestaurantDocumentDto>
{
    public RejectRestaurantDocumentDtoValidator()
    {
        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.DocumentRejectionReasonMaxLength)
            .WithMessage(ValidatorMessages.MaxLength);
    }
}
