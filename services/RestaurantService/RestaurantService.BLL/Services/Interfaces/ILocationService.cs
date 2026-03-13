using RestaurantService.BLL.DTOs;

namespace RestaurantService.BLL.Services.Interfaces;

public interface ILocationService
{
    Task<IEnumerable<LocationDto>> GetAllByRestaurantIdAsync(Guid restaurantId, CancellationToken cancellationToken = default);
    Task<LocationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(Guid restaurantId, CreateLocationDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, UpdateLocationDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
