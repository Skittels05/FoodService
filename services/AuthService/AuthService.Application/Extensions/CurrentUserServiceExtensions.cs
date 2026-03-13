using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Enums;

namespace AuthService.Application.Extensions;

public static class CurrentUserServiceExtensions
{

    public static void EnsureHasAccessToResource(this ICurrentUserService currentUserService, string targetAuth0Id)
    {
        var hasAccess = currentUserService.Role switch
        {
            UserRole.Admin => true,
            _ => currentUserService.Auth0Id == targetAuth0Id
        };

        if (!hasAccess)
        {
            throw new AccessDeniedException();
        }
    }

    public static void EnsureHasAccessToRestaurant(this ICurrentUserService user, Guid targetRestaurantId)
    {
        var hasAccess = user.Role switch
        {
            UserRole.Admin => true,
            UserRole.RestaurantManager => user.RestaurantId == targetRestaurantId,
            _ => false
        };

        if (!hasAccess)
        {
            throw new AccessDeniedException();
        }
    }

    public static void EnsureIsAdmin(this ICurrentUserService currentUserService)
    {
        if (currentUserService.Role is not UserRole.Admin)
        {
            throw new AccessDeniedException();
        }
    }

    public static void EnsureIsOwnerOrAdmin(this ICurrentUserService currentUserService, Guid currentUserId, Guid targetId)
    {
        if (currentUserService.Role is UserRole.Admin) return;

        if (currentUserId == targetId) return;

        throw new AccessDeniedException();
    }
}
