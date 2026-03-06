using AuthService.Application.Constants;
using AuthService.Application.Interfaces;
using AuthService.Domain.Enums;
using System.Security.Claims;

namespace AuthService.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Lazy<string?> _auth0Id;
    private readonly Lazy<string?> _email;
    private readonly Lazy<string?> _username;
    private readonly Lazy<UserRole> _role;
    private readonly Lazy<bool> _isVerified;
    private readonly Lazy<Guid?> _restaurantId;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        _auth0Id = new Lazy<string?>(() => User?.FindFirstValue(ClaimTypes.NameIdentifier));

        _email = new Lazy<string?>(() => User?.FindFirstValue(AppClaimTypes.Email));

        _username = new Lazy<string?>(() => User?.FindFirstValue(AppClaimTypes.Username));

        _role = new Lazy<UserRole>(() =>
        {
            var roleClaim = User?.FindFirstValue(AppClaimTypes.Role);
            return Enum.TryParse<UserRole>(roleClaim, true, out var role) ? role : UserRole.None;
        });

        _isVerified = new Lazy<bool>(() =>
        {
            var verifiedClaim = User?.FindFirstValue(AppClaimTypes.IsVerified);
            return bool.TryParse(verifiedClaim, out var isVerified) && isVerified;
        });

        _restaurantId = new Lazy<Guid?>(() =>
        {
            var claim = User?.FindFirstValue(AppClaimTypes.RestaurantId);
            return Guid.TryParse(claim, out var id) ? id : null;
        });
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    public string? Auth0Id => _auth0Id.Value;
    public string? Email => _email.Value;
    public string? Username => _username.Value;
    public UserRole Role => _role.Value;
    public bool IsVerified => _isVerified.Value;
    public Guid? RestaurantId => _restaurantId.Value;
}
