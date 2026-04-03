using RestaurantService.BLL.Common;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Models;

namespace RestaurantService.BLL.Services.Interfaces;

public interface ILocationService
{
    Task<PagedList<LocationDto>> GetAllByRestaurantIdAsync(Guid restaurantId, PageRequest request, CancellationToken cancellationToken = default);
    Task<LocationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<RestaurantNearbyDto>> GetNearbyAsync(double latitude, double longitude, double radiusKm, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(Guid restaurantId, CreateLocationDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, UpdateLocationDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
