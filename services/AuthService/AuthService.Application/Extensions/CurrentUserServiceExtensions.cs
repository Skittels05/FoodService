using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Enums;

namespace AuthService.Application.Extensions;

public static class CurrentUserServiceExtensions
{

    public static void EnsureHasAccessToResource(this ICurrentUserService currentUserService, string targetAuth0Id)
    {
        if (currentUserService.Role != UserRole.Admin && currentUserService.Auth0Id != targetAuth0Id)
        {
            throw new AccessDeniedException();
        }
    }
}
