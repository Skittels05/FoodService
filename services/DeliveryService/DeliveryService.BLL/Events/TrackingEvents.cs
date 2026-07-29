namespace DeliveryService.BLL.Events;

public record CourierLocationUpdatedEvent(
    Guid CourierId, 
    Guid? ActiveOrderId, 
    double Latitude, 
    double Longitude, 
    double? Speed, 
    DateTime Timestamp);
