using System.Globalization;
using DeliveryService.BLL.Enums;
using DeliveryService.BLL.Models;
using DeliveryService.BLL.Services.Interfaces;
using StackExchange.Redis;

namespace DeliveryService.DAL.Integrations.Redis;

public sealed class RedisCourierLocationStore(IConnectionMultiplexer redis) : ICourierLocationStore
{
    private static readonly RedisKey AvailableKey = "{couriers}:available";
    private static readonly RedisKey DeliveringKey = "{couriers}:delivering";
    private const string StateKeyPrefix = "{couriers}:state:";

    private const string StatusField = "status";
    private const string LastSeenField = "lastSeen";

    private static readonly RedisValue AvailableStatus = CourierStatus.Available.ToString();
    private static readonly RedisValue DeliveringStatus = CourierStatus.Delivering.ToString();
    private static readonly RedisValue OfflineStatus = CourierStatus.Offline.ToString();

    private readonly IDatabase _db = redis.GetDatabase();

    public async Task SetAvailableAsync(Guid courierId, CourierLocation location, CancellationToken cancellationToken = default)
    {
        await _db.ScriptEvaluateAsync(CourierLocationScripts.SetAvailable, new
        {
            stateKey = StateKey(courierId),
            availableKey = AvailableKey,
            deliveringKey = DeliveringKey,
            member = Member(courierId),
            longitude = location.Longitude,
            latitude = location.Latitude,
            timestamp = location.Timestamp.ToUnixTimeMilliseconds(),
            status = AvailableStatus
        });
    }

    public async Task SetDeliveringAsync(Guid courierId, CourierLocation? location = null, CancellationToken cancellationToken = default)
    {
        if (location is null)
        {
            await _db.ScriptEvaluateAsync(CourierLocationScripts.MoveToDelivering, new
            {
                stateKey = StateKey(courierId),
                availableKey = AvailableKey,
                deliveringKey = DeliveringKey,
                member = Member(courierId),
                status = DeliveringStatus
            });

            return;
        }

        await _db.ScriptEvaluateAsync(CourierLocationScripts.SetDelivering, new
        {
            stateKey = StateKey(courierId),
            availableKey = AvailableKey,
            deliveringKey = DeliveringKey,
            member = Member(courierId),
            longitude = location.Longitude,
            latitude = location.Latitude,
            timestamp = location.Timestamp.ToUnixTimeMilliseconds(),
            status = DeliveringStatus
        });
    }

    public async Task SetOfflineAsync(Guid courierId, CancellationToken cancellationToken = default)
    {
        await _db.ScriptEvaluateAsync(CourierLocationScripts.SetOffline, new
        {
            stateKey = StateKey(courierId),
            availableKey = AvailableKey,
            deliveringKey = DeliveringKey,
            member = Member(courierId),
            status = OfflineStatus
        });
    }

    public async Task<LocationUpdateResult> UpdateLocationAsync(Guid courierId, CourierLocation location, CancellationToken cancellationToken = default)
    {
        var result = await _db.ScriptEvaluateAsync(CourierLocationScripts.UpdateLocation, new
        {
            stateKey = StateKey(courierId),
            availableKey = AvailableKey,
            deliveringKey = DeliveringKey,
            member = Member(courierId),
            longitude = location.Longitude,
            latitude = location.Latitude,
            timestamp = location.Timestamp.ToUnixTimeMilliseconds(),
            availableStatus = AvailableStatus,
            deliveringStatus = DeliveringStatus
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
        var state = await _db.HashGetAsync(StateKey(courierId), [StatusField, LastSeenField]);
        var status = state[0];
        var lastSeen = state[1];

        if (lastSeen.IsNull || (status != AvailableStatus && status != DeliveringStatus))
        {
            return null;
        }

        var key = status == AvailableStatus ? AvailableKey : DeliveringKey;
        var position = await _db.GeoPositionAsync(key, Member(courierId));

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
            key: AvailableKey,
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

    private static RedisKey StateKey(Guid courierId) => StateKeyPrefix + courierId;

    private static RedisValue Member(Guid courierId) => courierId.ToString();
}
