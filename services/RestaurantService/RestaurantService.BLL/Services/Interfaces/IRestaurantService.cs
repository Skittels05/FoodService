using RestaurantService.BLL.Common;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Models;

namespace RestaurantService.BLL.Services.Interfaces;

public interface IRestaurantService
{
    Task<PagedList<RestaurantDto>> GetAllAsync(PageRequest request, CancellationToken cancellationToken = default);
    Task<RestaurantDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(CreateRestaurantDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, UpdateRestaurantDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateActiveStatusAsync(Guid restaurantId, bool isActive, CancellationToken cancellationToken = default);
    Task VerifyAsync(Guid restaurantId, CancellationToken cancellationToken = default);
}
