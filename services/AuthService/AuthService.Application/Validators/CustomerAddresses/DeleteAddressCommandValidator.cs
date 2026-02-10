using FluentValidation;
using AuthService.Application.CQRS.CustomerAddresses.Commands;

namespace AuthService.Application.Validators.CustomerAddresses;

public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
{
    public DeleteAddressCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Address ID is required.");
    }
}
