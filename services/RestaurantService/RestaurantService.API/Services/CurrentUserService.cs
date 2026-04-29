using System.Security.Claims;
using RestaurantService.API.Constants;
using RestaurantService.BLL.Interfaces;

namespace RestaurantService.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    private readonly Lazy<string?> _auth0Id;
    private readonly Lazy<string?> _email;
    private readonly Lazy<string?> _username;
    private readonly Lazy<bool> _isVerified;
    private readonly Lazy<Guid?> _restaurantId;
    private readonly Lazy<string?> _role;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        _auth0Id = new Lazy<string?>(() => User?.FindFirstValue(ClaimTypes.NameIdentifier));
        _email = new Lazy<string?>(() => User?.FindFirstValue(CustomClaims.Email));
        _username = new Lazy<string?>(() => User?.FindFirstValue(CustomClaims.Username));

        _isVerified = new Lazy<bool>(() =>
        {
            var claim = User?.FindFirstValue(CustomClaims.IsVerified);
            return bool.TryParse(claim, out var verified) && verified;
        });

        _restaurantId = new Lazy<Guid?>(() =>
        {
            var claim = User?.FindFirstValue(CustomClaims.RestaurantId);
            return Guid.TryParse(claim, out var id) ? id : null;
        });

        _role = new Lazy<string?>(() => User?.FindFirstValue(CustomClaims.Roles));
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public string? Auth0Id => _auth0Id.Value;
    public string? Email => _email.Value;
    public string? Username => _username.Value;
    public bool IsVerified => _isVerified.Value;
    public Guid? RestaurantId => _restaurantId.Value;
    public string? Role => _role.Value;
}
