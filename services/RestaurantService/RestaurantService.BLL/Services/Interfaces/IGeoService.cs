using RestaurantService.BLL.DTOs;

namespace RestaurantService.BLL.Services.Interfaces;

public interface IGeoService
{
    Task AddOrUpdateLocationAsync(Guid locationId, double longitude, double latitude);

    Task<IEnumerable<GeoSearchResultDto>> GetLocationsNearAsync(double longitude, double latitude, double radiusInKm, int count = 50);

    Task RemoveLocationAsync(Guid locationId);
}
