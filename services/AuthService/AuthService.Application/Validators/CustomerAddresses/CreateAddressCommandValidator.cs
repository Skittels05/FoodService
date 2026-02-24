using AuthService.Application.Constants;
using AuthService.Application.CQRS.CustomerAddresses.Commands;
using AuthService.Domain.Constants;
using FluentValidation;

namespace AuthService.Application.Validators.CustomerAddresses;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage(ValidatorMessages.Required);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage(ValidatorMessages.Required)
            .MaximumLength(ValidationConstants.AddressMaxLength).WithMessage(ValidatorMessages.MaxLength);
    }
}
