using DeliveryService.BLL.Enums;
using StackExchange.Redis;

namespace DeliveryService.DAL.Integrations.Redis;

internal static class CourierRedisKeys
{
    public static readonly RedisKey Available = "{couriers}:available";
    public static readonly RedisKey Offered = "{couriers}:offered";
    public static readonly RedisKey Delivering = "{couriers}:delivering";

    public static readonly RedisValue AvailableStatus = nameof(CourierStatus.Available);
    public static readonly RedisValue OfferedStatus = nameof(CourierStatus.Offered);
    public static readonly RedisValue DeliveringStatus = nameof(CourierStatus.Delivering);
    public static readonly RedisValue OfflineStatus = nameof(CourierStatus.Offline);

    public static RedisKey State(Guid courierId) => $"{{couriers}}:state:{courierId}";

    public static RedisKey Offer(Guid orderId) => $"{{couriers}}:offer:{orderId}";

    public static RedisKey Declined(Guid orderId) => $"{{couriers}}:declined:{orderId}";

    public static RedisValue Member(Guid courierId) => courierId.ToString();
}
