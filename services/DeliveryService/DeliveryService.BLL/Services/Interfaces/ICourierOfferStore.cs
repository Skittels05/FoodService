namespace DeliveryService.BLL.Services.Interfaces;

public interface ICourierOfferStore
{
    Task<bool> TryCreateOfferAsync(Guid orderId, Guid courierId, TimeSpan timeToLive, CancellationToken cancellationToken = default);
    Task<bool> HasActiveOfferAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<IReadOnlySet<Guid>> GetDeclinedCouriersAsync(Guid orderId, CancellationToken cancellationToken = default);
}
