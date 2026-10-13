using DeliveryService.BLL.Constants;
using DeliveryService.BLL.Enums;
using DeliveryService.BLL.Events;
using DeliveryService.BLL.Repositories.Interfaces;
using DeliveryService.BLL.Services.Interfaces;

namespace DeliveryService.BLL.Services;

public class CourierSearchService(
    IOrderRepository orderRepository,
    ICourierLocationStore courierLocationStore,
    ICourierOfferStore courierOfferStore,
    ICourierSearchScheduler courierSearchScheduler,
    IOutboxWriter outboxWriter,
    IUnitOfWork unitOfWork,
    TimeProvider timeProvider) : ICourierSearchService
{
    private static readonly double[] SearchRadiiInKm = [3, 5, 10];

    private static readonly TimeSpan OfferTimeToLive =
        TimeSpan.FromSeconds(Constraints.CourierOfferTimeToLiveInSeconds);

    private static readonly TimeSpan RetryDelay =
        TimeSpan.FromSeconds(Constraints.CourierSearchRetryDelayInSeconds);

    public async Task SearchAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(orderId, cancellationToken: cancellationToken);

        if (order is null
            || order.CourierId is not null
            || order.Status is not (OrderStatus.Preparing or OrderStatus.ReadyForPickup))
        {
            return;
        }

        if (await courierOfferStore.HasActiveOfferAsync(orderId, cancellationToken))
        {
            return;
        }

        var excluded = new HashSet<Guid>(await courierOfferStore.GetDeclinedCouriersAsync(orderId, cancellationToken));

        foreach (var radius in SearchRadiiInKm)
        {
            var candidates = await courierLocationStore.FindNearestAvailableAsync(
                order.Pickup.Latitude,
                order.Pickup.Longitude,
                radius,
                Constraints.CourierSearchCandidatesCount + excluded.Count,
                cancellationToken);

            foreach (var candidate in candidates)
            {
                if (!excluded.Add(candidate.CourierId))
                {
                    continue;
                }

                if (await courierOfferStore.TryCreateOfferAsync(orderId, candidate.CourierId, OfferTimeToLive, cancellationToken))
                {
                    outboxWriter.Write(new CourierOfferedEvent(
                        orderId,
                        candidate.CourierId,
                        timeProvider.GetUtcNow() + OfferTimeToLive));

                    await unitOfWork.SaveChangesAsync(cancellationToken);
                    return;
                }
            }
        }

        await courierSearchScheduler.ScheduleAsync(orderId, RetryDelay, cancellationToken);
    }
}
