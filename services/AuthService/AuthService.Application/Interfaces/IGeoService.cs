namespace AuthService.Application.Interfaces;

public interface IGeoService
{
    Task AddOrUpdateLocationAsync(Guid addressId, double longitude, double latitude);
    Task RemoveLocationAsync(Guid addressId);
}
