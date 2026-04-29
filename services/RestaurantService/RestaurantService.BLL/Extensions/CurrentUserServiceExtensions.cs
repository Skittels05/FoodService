using RestaurantService.BLL.Exceptions;
using RestaurantService.BLL.Interfaces;

namespace RestaurantService.BLL.Extensions;

public static class CurrentUserServiceExtensions
{
    public static void EnsureHasAccessToRestaurant(this ICurrentUserService user, Guid targetRestaurantId)
    {
        var hasAccess = user.Role switch
        {
            "Admin" => true,
            "RestaurantManager" => user.RestaurantId == targetRestaurantId,
            _ => false
        };

        if (!hasAccess)
        {
            throw new AccessDeniedException();
        }
    }
}
