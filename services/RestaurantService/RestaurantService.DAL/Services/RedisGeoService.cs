using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Services.Interfaces;
using StackExchange.Redis;

namespace RestaurantService.DAL.Redis;

public class RedisGeoService(IConnectionMultiplexer redis) : IGeoService
{
    private readonly IDatabase _db = redis.GetDatabase();
    private const string GeoKey = "restaurants:locations";

    public async Task AddOrUpdateLocationAsync(Guid locationId, double longitude, double latitude)
    {
        await _db.GeoAddAsync(GeoKey, longitude, latitude, locationId.ToString());
    }

    public async Task<IEnumerable<GeoSearchResultDto>> GetLocationsNearAsync(double longitude, double latitude, double radiusInKm, int count = 50)
    {
        var results = await _db.GeoSearchAsync(
            key: GeoKey,
            longitude: longitude,
            latitude: latitude,
            shape: new GeoSearchCircle(radiusInKm, GeoUnit.Kilometers),
            count: count,
            order: Order.Ascending,
            options: GeoRadiusOptions.WithDistance
        );

        if (results is null or [])
            return [];

        return results.Select(r => new GeoSearchResultDto(
            LocationId: Guid.Parse(r.Member.ToString()),
            Distance: r.Distance ?? 0
        ));
    }

    public async Task RemoveLocationAsync(Guid locationId)
    {
        await _db.SortedSetRemoveAsync(GeoKey, locationId.ToString());
    }
}
