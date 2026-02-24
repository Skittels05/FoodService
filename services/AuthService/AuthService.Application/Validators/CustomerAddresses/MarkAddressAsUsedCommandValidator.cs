using FluentValidation;
using AuthService.Application.CQRS.CustomerAddresses.Commands;

namespace AuthService.Application.Validators.CustomerAddresses;

public class MarkAddressAsUsedCommandValidator : AbstractValidator<MarkAddressAsUsedCommand>
{
    public MarkAddressAsUsedCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);
    }
}
