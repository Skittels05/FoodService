namespace DeliveryService.BLL.Services.Interfaces;

public interface IOutboxWriter
{
    void Write<TEvent>(TEvent @event) where TEvent : class;
}
