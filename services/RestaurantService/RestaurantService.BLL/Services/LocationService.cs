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
    IMappingService mappingService,
    IGeoService geoService) : ILocationService
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

    public async Task<IEnumerable<RestaurantNearbyDto>> GetNearbyAsync(double latitude, double longitude, double radiusKm, CancellationToken cancellationToken = default)
    {
        var geoResults = await geoService.GetLocationsNearAsync(longitude, latitude, radiusKm);

        if (geoResults is null || !geoResults.Any())
            return [];

        var locationIds = geoResults.Select(r => r.LocationId).ToList();
        var locations = await locationRepository.GetByIdsWithRestaurantAsync(locationIds, cancellationToken);

        var result = locations.Select(loc =>
        {
            var distance = geoResults.First(r => r.LocationId == loc.Id).Distance;

            return new RestaurantNearbyDto(
                LocationId: loc.Id,
                RestaurantId: loc.RestaurantId,
                RestaurantName: loc.Restaurant?.Name,
                Address: loc.Address,
                DistanceInKm: Math.Round(distance, 2),
                Latitude: loc.Latitude,
                Longitude: loc.Longitude
            );
        })
        .OrderBy(r => r.DistanceInKm);

        return result;
    }

    public async Task<Guid> CreateAsync(Guid restaurantId, CreateLocationDto dto, CancellationToken cancellationToken = default)
    {
        _ = await restaurantRepository.GetByIdAsync(restaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), restaurantId);

        var location = mappingService.Map<CreateLocationDto, Location>(dto);
        location.RestaurantId = restaurantId;

        await locationRepository.AddAsync(location, cancellationToken);

        await geoService.AddOrUpdateLocationAsync(location.Id, location.Longitude, location.Latitude);

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
        await geoService.AddOrUpdateLocationAsync(location.Id, location.Longitude, location.Latitude);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var isDeleted = await locationRepository.DeleteAsync(id, cancellationToken);
        if (isDeleted)
        {
            await geoService.RemoveLocationAsync(id);
        }

        return isDeleted;
    }
}
