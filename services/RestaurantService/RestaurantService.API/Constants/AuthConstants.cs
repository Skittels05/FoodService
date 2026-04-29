namespace RestaurantService.API.Constants;

public static class CustomClaims
{
    public const string Roles = "https://food-service.com/roles";
    public const string RestaurantId = "https://food-service.com/restaurant_id";
    public const string Username = "https://food-service.com/username";
    public const string Email = "https://food-service.com/email";
    public const string IsVerified = "https://food-service.com/is_verified";
}

public static class Policies
{
    public const string AdminOnly = "AdminOnly";
    public const string RestaurantManager = "RestaurantManager";
    public const string ManagerOrAdmin = "ManagerOrAdmin";
}
