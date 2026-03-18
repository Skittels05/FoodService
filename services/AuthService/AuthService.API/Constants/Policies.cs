namespace AuthService.API.Constants
{
    public static class Policies
    {
        public const string AdminOnly = nameof(AdminOnly);
        public const string CourierOrAdmin = nameof(CourierOrAdmin);
        public const string CustomerOrAdmin = nameof(CustomerOrAdmin);
        public const string RestaurantManagerOrAdmin = nameof(RestaurantManagerOrAdmin);
    }
}
