using AuthService.Application.Constants;
using AuthService.Application.Interfaces;
using AuthService.Domain.Enums;
using System.Security.Claims;

namespace AuthService.API.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public string? Auth0Id => User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? Email => User?.FindFirstValue(AppClaimTypes.Email);
    public string? Username => User?.FindFirstValue(AppClaimTypes.Username);
    public UserRole Role
    {
        get
        {
            var roleClaim = User?.FindFirstValue(AppClaimTypes.Role);
            if (Enum.TryParse<UserRole>(roleClaim, true, out var role))
            {
                return role;
            }

            return UserRole.None;
        }
    }
    public bool IsVerified
    {
        get
        {
            var verifiedClaim = User?.FindFirstValue(AppClaimTypes.IsVerified);
            return bool.TryParse(verifiedClaim, out var isVerified) && isVerified;
        }
    }

    public Guid? RestaurantId
    {
        get
        {
            var claim = User?.FindFirstValue(AppClaimTypes.RestaurantId);
            return Guid.TryParse(claim, out var id) ? id : null;
        }
    }
}
