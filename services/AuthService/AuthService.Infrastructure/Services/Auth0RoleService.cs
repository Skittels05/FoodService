using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using AuthService.Application.Interfaces;
using AuthService.Domain.Enums;
using AuthService.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace AuthService.Infrastructure.Services;

public class Auth0RoleService(IOptions<Auth0ManagementSettings> options) : IAuth0RoleService
{
    private readonly Auth0ManagementSettings _settings = options.Value;

    public async Task AssignRoleAsync(string auth0UserId, UserRole role, CancellationToken cancellationToken)
    {
        var roleName = role.ToString();
        if (!_settings.Roles.TryGetValue(roleName, out var roleId))
        {
            throw new InvalidOperationException($"Role ID for '{roleName}' is not configured in appsettings.json.");
        }
        var authClient = new AuthenticationApiClient(new Uri($"https://{_settings.Domain}/"));
        var tokenResponse = await authClient.GetTokenAsync(new ClientCredentialsTokenRequest
        {
            Audience = $"https://{_settings.Domain}/api/v2/",
            ClientId = _settings.ClientId,
            ClientSecret = _settings.ClientSecret
        }, cancellationToken);

        var managementClient = new ManagementApiClient(tokenResponse.AccessToken, new Uri($"https://{_settings.Domain}/api/v2/"));
        var request = new AssignRolesRequest
        {
            Roles = new[] { roleId }
        };

        await managementClient.Users.AssignRolesAsync(auth0UserId, request, cancellationToken);
    }
}
