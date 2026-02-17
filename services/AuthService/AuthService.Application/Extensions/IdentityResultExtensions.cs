using AuthService.Application.Exceptions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.Extensions;

public static class IdentityResultExtensions
{
    public static void EnsureSuccess(this IdentityResult result)
    {
        if (result.Succeeded) return;

        var failures = result.Errors.Select(e =>
            new ValidationFailure(e.Code, e.Description));

        throw new ValidationException(failures);
    }
}
