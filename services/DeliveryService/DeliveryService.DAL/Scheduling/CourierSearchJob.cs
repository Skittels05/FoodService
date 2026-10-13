using DeliveryService.BLL.Services.Interfaces;
using Quartz;

namespace DeliveryService.DAL.Scheduling;

public class CourierSearchJob(ICourierSearchService courierSearchService) : IJob
{
    public static readonly JobKey Key = new(nameof(CourierSearchJob));

    public const string OrderIdKey = "orderId";

    public async ValueTask Execute(IJobExecutionContext context, CancellationToken cancellationToken)
    {
        var orderId = Guid.Parse(context.MergedJobDataMap.GetString(OrderIdKey)!);

        await courierSearchService.SearchAsync(orderId, cancellationToken);
    }
}
