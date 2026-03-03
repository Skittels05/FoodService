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
        if (role is UserRole.None)
        {
            return;
        }

        var roleId = _settings.Roles[role.ToString()];
        var managementClient = await GetManagementApiClientAsync(cancellationToken);
        var request = new AssignRolesRequest
        {
            Roles = [roleId]
        };

        await managementClient.Users.AssignRolesAsync(auth0UserId, request, cancellationToken);
    }

    public async Task SetAsVerifiedAsync(string auth0UserId, CancellationToken cancellationToken = default)
    {
        var managementClient = await GetManagementApiClientAsync(cancellationToken);
        var request = new UserUpdateRequest
        {
            AppMetadata = new { is_verified = true }
        };

        await managementClient.Users.UpdateAsync(auth0UserId, request, cancellationToken);
    }

    private async Task<ManagementApiClient> GetManagementApiClientAsync(CancellationToken cancellationToken)
    {
        var authClient = new AuthenticationApiClient(new Uri($"https://{_settings.Domain}/"));
        var tokenResponse = await authClient.GetTokenAsync(new ClientCredentialsTokenRequest
        {
            Audience = $"https://{_settings.Domain}/api/v2/",
            ClientId = _settings.ClientId,
            ClientSecret = _settings.ClientSecret
        }, cancellationToken);

        return new ManagementApiClient(tokenResponse.AccessToken, new Uri($"https://{_settings.Domain}/api/v2/"));
    }
}
