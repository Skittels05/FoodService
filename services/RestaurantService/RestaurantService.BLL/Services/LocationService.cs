using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Exceptions;
using RestaurantService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Repositories.Interfaces;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.BLL.Services;

public class LocationService(
    ILocationRepository locationRepository,
    IRestaurantRepository restaurantRepository,
    IMappingService mappingService) : ILocationService
{
    public async Task<IEnumerable<LocationDto>> GetAllByRestaurantIdAsync(Guid restaurantId, CancellationToken cancellationToken = default)
    {
        var locations = await locationRepository.GetAllByRestaurantIdAsync(restaurantId, cancellationToken);

        return locations.Select(mappingService.Map<Location, LocationDto>);
    }

    public async Task<LocationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var location = await locationRepository.GetByIdAsync(id, cancellationToken);
        return location is not null ? mappingService.Map<Location, LocationDto>(location) : null;
    }

    public async Task<Guid> CreateAsync(Guid restaurantId, CreateLocationDto dto, CancellationToken cancellationToken = default)
    {
        _ = await restaurantRepository.GetByIdAsync(restaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), restaurantId);

        var location = mappingService.Map<CreateLocationDto, Location>(dto);
        location.RestaurantId = restaurantId;

        await locationRepository.AddAsync(location, cancellationToken);
        return location.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateLocationDto dto, CancellationToken cancellationToken = default)
    {
        var location = await locationRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Location), id);

        location.Address = dto.Address;
        location.Latitude = dto.Latitude;
        location.Longitude = dto.Longitude;
        location.IsAcceptingOrders = dto.IsAcceptingOrders;

        await locationRepository.UpdateAsync(location, cancellationToken);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => locationRepository.DeleteAsync(id, cancellationToken);
}
