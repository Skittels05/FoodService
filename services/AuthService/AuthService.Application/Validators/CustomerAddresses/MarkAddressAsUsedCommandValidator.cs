using AuthService.Application.Constants;
using AuthService.Application.CQRS.CustomerAddresses.Commands;
using FluentValidation;

namespace AuthService.Application.Validators.CustomerAddresses;

public class MarkAddressAsUsedCommandValidator : AbstractValidator<MarkAddressAsUsedCommand>
{
    public MarkAddressAsUsedCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);
    }
}
