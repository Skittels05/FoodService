using RestaurantService.BLL.Common;
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
    public async Task<PagedList<LocationDto>> GetAllByRestaurantIdAsync(Guid restaurantId, PageRequest request, CancellationToken cancellationToken = default)
    {
        var pagedLocations = await locationRepository.GetAllByRestaurantIdAsync(restaurantId, request, cancellationToken);
        return mappingService.MapPagedList<Location, LocationDto>(pagedLocations);
    }

    public async Task<LocationDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var location = await locationRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Location), id);

        return mappingService.Map<Location, LocationDto>(location);
    }

    public async Task<IEnumerable<RestaurantNearbyDto>> GetNearbyAsync(GetNearbyLocationsDto dto, CancellationToken cancellationToken = default)
    {
        var geoResults = await geoService.GetLocationsNearAsync(dto.Longitude, dto.Latitude, dto.RadiusKm);

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
                RestaurantName: loc.Restaurant?.Name ?? string.Empty,
                Address: loc.Address,
                DistanceInKm: Math.Round(distance, 2),
                Latitude: loc.Latitude,
                Longitude: loc.Longitude
            );
        })
        .OrderBy(r => r.DistanceInKm);

        return result;
    }

    public async Task<Guid> CreateAsync(CreateLocationDto dto, CancellationToken cancellationToken = default)
    {
        _ = await restaurantRepository.GetByIdAsync(dto.RestaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), dto.RestaurantId);

        var location = mappingService.Map<CreateLocationDto, Location>(dto);
        location.RestaurantId = dto.RestaurantId;

        await locationRepository.AddAsync(location, cancellationToken);

        await geoService.AddOrUpdateLocationAsync(location.Id, location.Longitude, location.Latitude);

        return location.Id;
    }

    public async Task UpdateAsync(UpdateLocationDto dto, CancellationToken cancellationToken = default)
    {
        var location = await locationRepository.GetByIdAsync(dto.Id, cancellationToken, true)
            ?? throw new NotFoundException(nameof(Location), dto.Id);

        location.Address = dto.Address;
        location.Latitude = dto.Latitude;
        location.Longitude = dto.Longitude;
        location.IsAcceptingOrders = dto.IsAcceptingOrders;

        await locationRepository.UpdateAsync(location, cancellationToken);
        await geoService.AddOrUpdateLocationAsync(location.Id, location.Longitude, location.Latitude);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var isDeleted = await locationRepository.DeleteAsync(id, cancellationToken);
        
        if (!isDeleted)
            throw new NotFoundException(nameof(Location), id);

        await geoService.RemoveLocationAsync(id);
    }
}
