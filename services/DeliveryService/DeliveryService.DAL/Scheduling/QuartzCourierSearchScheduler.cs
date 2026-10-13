using DeliveryService.BLL.Services.Interfaces;
using Quartz;

namespace DeliveryService.DAL.Scheduling;

public sealed class QuartzCourierSearchScheduler(
    ISchedulerFactory schedulerFactory,
    TimeProvider timeProvider) : ICourierSearchScheduler
{
    public async Task ScheduleAsync(Guid orderId, TimeSpan? delay = null, CancellationToken cancellationToken = default)
    {
        var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

        var trigger = TriggerBuilder.Create()
            .ForJob(CourierSearchJob.Key)
            .UsingJobData(CourierSearchJob.OrderIdKey, orderId.ToString())
            .StartAt(timeProvider.GetUtcNow() + (delay ?? TimeSpan.Zero))
            .Build();

        await scheduler.ScheduleJob(trigger, cancellationToken: cancellationToken);
    }
}
