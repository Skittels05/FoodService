using AuthService.Application.Constants;
using AuthService.Application.Interfaces;
using System.Security.Claims;

namespace AuthService.API.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public string? Auth0Id => User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? Email => User?.FindFirstValue(AppClaimTypes.Email);
    public string? Username => User?.FindFirstValue(AppClaimTypes.Username);
    public string? Role => User?.FindFirstValue(AppClaimTypes.Role);
    public bool IsVerified
    {
        get
        {
            var verifiedClaim = User?.FindFirstValue(AppClaimTypes.IsVerified);
            return bool.TryParse(verifiedClaim, out var isVerified) && isVerified;
        }
    }
}