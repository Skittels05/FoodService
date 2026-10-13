using StackExchange.Redis;

namespace DeliveryService.DAL.Integrations.Redis;

internal static class CourierOfferScripts
{
    public static readonly LuaScript CreateOffer = LuaScript.Prepare("""
        if redis.call('HGET', @stateKey, 'status') ~= @availableStatus then
            return 0
        end

        if redis.call('EXISTS', @offerKey) == 1 then
            return 0
        end

        local score = redis.call('ZSCORE', @availableKey, @member)
        if not score then
            return 0
        end

        redis.call('ZREM', @availableKey, @member)
        redis.call('ZADD', @offeredKey, score, @member)
        redis.call('HSET', @stateKey, 'status', @offeredStatus)
        redis.call('SET', @offerKey, @member, 'PX', @ttl)
        return 1
        """);
}
