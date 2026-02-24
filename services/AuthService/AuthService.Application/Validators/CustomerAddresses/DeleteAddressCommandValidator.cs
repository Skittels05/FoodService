using AuthService.Application.Constants;
using AuthService.Application.CQRS.CustomerAddresses.Commands;
using FluentValidation;

namespace AuthService.Application.Validators.CustomerAddresses;

public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
{
    public DeleteAddressCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidatorMessages.Required);
    }
}
