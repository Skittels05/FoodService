using DeliveryService.BLL.Services.Interfaces;
using StackExchange.Redis;

namespace DeliveryService.DAL.Integrations.Redis;

public sealed class RedisCourierOfferStore(IConnectionMultiplexer redis) : ICourierOfferStore
{
    private readonly IDatabase _db = redis.GetDatabase();

    public async Task<bool> TryCreateOfferAsync(Guid orderId, Guid courierId, TimeSpan timeToLive, CancellationToken cancellationToken = default)
    {
        var result = await _db.ScriptEvaluateAsync(CourierOfferScripts.CreateOffer, new
        {
            stateKey = CourierRedisKeys.State(courierId),
            availableKey = CourierRedisKeys.Available,
            offeredKey = CourierRedisKeys.Offered,
            offerKey = CourierRedisKeys.Offer(orderId),
            member = CourierRedisKeys.Member(courierId),
            availableStatus = CourierRedisKeys.AvailableStatus,
            offeredStatus = CourierRedisKeys.OfferedStatus,
            ttl = (long)timeToLive.TotalMilliseconds
        });

        return (int)result == 1;
    }

    public Task<bool> HasActiveOfferAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return _db.KeyExistsAsync(CourierRedisKeys.Offer(orderId));
    }

    public async Task<IReadOnlySet<Guid>> GetDeclinedCouriersAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var members = await _db.SetMembersAsync(CourierRedisKeys.Declined(orderId));

        return members
            .Select(m => Guid.Parse(m.ToString()))
            .ToHashSet();
    }
}
