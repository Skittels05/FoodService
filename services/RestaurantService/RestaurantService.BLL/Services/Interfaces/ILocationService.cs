using RestaurantService.BLL.Common;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Models;

namespace RestaurantService.BLL.Services.Interfaces;

public interface ILocationService
{
    Task<PagedList<LocationDto>> GetAllByRestaurantIdAsync(Guid restaurantId, PageRequest request, CancellationToken cancellationToken = default);
    Task<LocationDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<RestaurantNearbyDto>> GetNearbyAsync(GetNearbyLocationsDto dto, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(CreateLocationDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateLocationDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
