using System.Collections.Frozen;
using System.Globalization;
using DeliveryService.BLL.Enums;
using DeliveryService.BLL.Models;
using DeliveryService.BLL.Services.Interfaces;
using StackExchange.Redis;

namespace DeliveryService.DAL.Integrations.Redis;

public sealed class RedisCourierLocationStore(IConnectionMultiplexer redis) : ICourierLocationStore
{
    private const string StatusField = "status";
    private const string LastSeenField = "lastSeen";

    private static readonly FrozenDictionary<RedisValue, RedisKey> LocationKeys = new Dictionary<RedisValue, RedisKey>
    {
        [CourierRedisKeys.AvailableStatus] = CourierRedisKeys.Available,
        [CourierRedisKeys.OfferedStatus] = CourierRedisKeys.Offered,
        [CourierRedisKeys.DeliveringStatus] = CourierRedisKeys.Delivering
    }.ToFrozenDictionary();

    private readonly IDatabase _db = redis.GetDatabase();

    public async Task SetAvailableAsync(Guid courierId, CourierLocation location, CancellationToken cancellationToken = default)
    {
        await _db.ScriptEvaluateAsync(CourierLocationScripts.SetAvailable, new
        {
            stateKey = CourierRedisKeys.State(courierId),
            availableKey = CourierRedisKeys.Available,
            offeredKey = CourierRedisKeys.Offered,
            deliveringKey = CourierRedisKeys.Delivering,
            member = CourierRedisKeys.Member(courierId),
            longitude = location.Longitude,
            latitude = location.Latitude,
            timestamp = location.Timestamp.ToUnixTimeMilliseconds(),
            status = CourierRedisKeys.AvailableStatus
        });
    }

    public async Task SetDeliveringAsync(Guid courierId, CourierLocation? location = null, CancellationToken cancellationToken = default)
    {
        if (location is null)
        {
            await _db.ScriptEvaluateAsync(CourierLocationScripts.MoveToDelivering, new
            {
                stateKey = CourierRedisKeys.State(courierId),
                availableKey = CourierRedisKeys.Available,
                offeredKey = CourierRedisKeys.Offered,
                deliveringKey = CourierRedisKeys.Delivering,
                member = CourierRedisKeys.Member(courierId),
                status = CourierRedisKeys.DeliveringStatus
            });

            return;
        }

        await _db.ScriptEvaluateAsync(CourierLocationScripts.SetDelivering, new
        {
            stateKey = CourierRedisKeys.State(courierId),
            availableKey = CourierRedisKeys.Available,
            offeredKey = CourierRedisKeys.Offered,
            deliveringKey = CourierRedisKeys.Delivering,
            member = CourierRedisKeys.Member(courierId),
            longitude = location.Longitude,
            latitude = location.Latitude,
            timestamp = location.Timestamp.ToUnixTimeMilliseconds(),
            status = CourierRedisKeys.DeliveringStatus
        });
    }

    public async Task SetOfflineAsync(Guid courierId, CancellationToken cancellationToken = default)
    {
        await _db.ScriptEvaluateAsync(CourierLocationScripts.SetOffline, new
        {
            stateKey = CourierRedisKeys.State(courierId),
            availableKey = CourierRedisKeys.Available,
            offeredKey = CourierRedisKeys.Offered,
            deliveringKey = CourierRedisKeys.Delivering,
            member = CourierRedisKeys.Member(courierId),
            status = CourierRedisKeys.OfflineStatus
        });
    }

    public async Task<LocationUpdateResult> UpdateLocationAsync(Guid courierId, CourierLocation location, CancellationToken cancellationToken = default)
    {
        var result = await _db.ScriptEvaluateAsync(CourierLocationScripts.UpdateLocation, new
        {
            stateKey = CourierRedisKeys.State(courierId),
            availableKey = CourierRedisKeys.Available,
            offeredKey = CourierRedisKeys.Offered,
            deliveringKey = CourierRedisKeys.Delivering,
            member = CourierRedisKeys.Member(courierId),
            longitude = location.Longitude,
            latitude = location.Latitude,
            timestamp = location.Timestamp.ToUnixTimeMilliseconds(),
            availableStatus = CourierRedisKeys.AvailableStatus,
            offeredStatus = CourierRedisKeys.OfferedStatus,
            deliveringStatus = CourierRedisKeys.DeliveringStatus
        });

        return (int)result switch
        {
            1 => LocationUpdateResult.Accepted,
            -1 => LocationUpdateResult.CourierNotTracked,
            _ => LocationUpdateResult.Rejected
        };
    }

    public async Task<CourierLocation?> GetLocationAsync(Guid courierId, CancellationToken cancellationToken = default)
    {
        var state = await _db.HashGetAsync(CourierRedisKeys.State(courierId), [StatusField, LastSeenField]);
        var status = state[0];
        var lastSeen = state[1];

        if (lastSeen.IsNull || !LocationKeys.TryGetValue(status, out var key))
        {
            return null;
        }

        var position = await _db.GeoPositionAsync(key, CourierRedisKeys.Member(courierId));

        if (position is null)
        {
            return null;
        }

        return new CourierLocation(
            position.Value.Latitude,
            position.Value.Longitude,
            DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(lastSeen.ToString(), CultureInfo.InvariantCulture)));
    }

    public async Task<IReadOnlyList<NearbyCourier>> FindNearestAvailableAsync(
        double latitude,
        double longitude,
        double radiusKm,
        int count,
        CancellationToken cancellationToken = default)
    {
        var results = await _db.GeoSearchAsync(
            key: CourierRedisKeys.Available,
            longitude: longitude,
            latitude: latitude,
            shape: new GeoSearchCircle(radiusKm, GeoUnit.Kilometers),
            count: count,
            order: StackExchange.Redis.Order.Ascending,
            options: GeoRadiusOptions.WithDistance);

        return results
            .Select(r => new NearbyCourier(Guid.Parse(r.Member.ToString()), r.Distance ?? 0))
            .ToList();
    }
}
