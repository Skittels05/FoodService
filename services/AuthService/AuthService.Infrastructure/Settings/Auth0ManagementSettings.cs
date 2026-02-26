namespace AuthService.Infrastructure.Settings;

public class Auth0ManagementSettings
{
    public string Domain { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string CourierRoleId { get; set; } = string.Empty;
    public string CustomerRoleId { get; set; } = string.Empty;
    public string RestaurantManagerRoleId { get; set; } = string.Empty;
}
