namespace AuthService.Application.Constants;

public static class AppClaimTypes
{
    private const string BaseNamespace = "https://food-service.com";
    public const string Email = $"{BaseNamespace}/email";
    public const string Username = $"{BaseNamespace}/username";
    public const string Roles = $"{BaseNamespace}/roles";
}
