using StackExchange.Redis;

namespace DeliveryService.DAL.Integrations.Redis;

internal static class CourierLocationScripts
{
    public static readonly LuaScript UpdateLocation = LuaScript.Prepare("""
        local status = redis.call('HGET', @stateKey, 'status')
        if not status then
            return -1
        end

        local key
        if status == @availableStatus then
            key = @availableKey
        elseif status == @deliveringStatus then
            key = @deliveringKey
        else
            return 0
        end

        local lastSeen = tonumber(redis.call('HGET', @stateKey, 'lastSeen'))
        if lastSeen and tonumber(@timestamp) <= lastSeen then
            return 0
        end

        redis.call('GEOADD', key, @longitude, @latitude, @member)
        redis.call('HSET', @stateKey, 'lastSeen', @timestamp)
        return 1
        """);

    public static readonly LuaScript SetAvailable = LuaScript.Prepare("""
        redis.call('ZREM', @deliveringKey, @member)
        redis.call('GEOADD', @availableKey, @longitude, @latitude, @member)
        redis.call('HSET', @stateKey, 'status', @status, 'lastSeen', @timestamp)
        return 1
        """);

    public static readonly LuaScript MoveToDelivering = LuaScript.Prepare("""
        local score = redis.call('ZSCORE', @availableKey, @member)
        redis.call('ZREM', @availableKey, @member)
        if score then
            redis.call('ZADD', @deliveringKey, score, @member)
        end
        redis.call('HSET', @stateKey, 'status', @status)
        return 1
        """);

    public static readonly LuaScript SetDelivering = LuaScript.Prepare("""
        redis.call('ZREM', @availableKey, @member)
        redis.call('GEOADD', @deliveringKey, @longitude, @latitude, @member)
        redis.call('HSET', @stateKey, 'status', @status, 'lastSeen', @timestamp)
        return 1
        """);

    public static readonly LuaScript SetOffline = LuaScript.Prepare("""
        redis.call('ZREM', @availableKey, @member)
        redis.call('ZREM', @deliveringKey, @member)
        redis.call('HSET', @stateKey, 'status', @status)
        return 1
        """);
}
