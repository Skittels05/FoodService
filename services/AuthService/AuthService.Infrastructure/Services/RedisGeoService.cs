using AuthService.Application.Interfaces;
using StackExchange.Redis;

namespace AuthService.Infrastructure.Services;

public class RedisGeoService(IConnectionMultiplexer redis) : IGeoService
{
    private readonly IDatabase _db = redis.GetDatabase();
    private const string GeoKey = "customers:addresses";

    public async Task AddOrUpdateLocationAsync(Guid addressId, double longitude, double latitude)
    {
        await _db.GeoAddAsync(GeoKey, longitude, latitude, addressId.ToString());
    }

    public async Task RemoveLocationAsync(Guid addressId)
    {
        await _db.SortedSetRemoveAsync(GeoKey, addressId.ToString());
    }
}
