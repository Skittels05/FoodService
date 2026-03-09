using RestaurantService.BLL.DTOs.Restaurant;

namespace RestaurantService.BLL.Services.Interfaces;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RestaurantDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(CreateRestaurantDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, UpdateRestaurantDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateActiveStatusAsync(Guid restaurantId, bool isActive, CancellationToken cancellationToken = default);
    Task VerifyAsync(Guid restaurantId, CancellationToken cancellationToken = default);
}
