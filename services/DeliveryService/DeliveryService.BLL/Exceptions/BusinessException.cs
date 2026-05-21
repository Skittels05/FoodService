namespace DeliveryService.BLL.Exceptions;

public abstract class BusinessException(string message) : Exception(message);

public class NotFoundException(string name, Guid id) 
    : BusinessException($"{name} with id {id} not found.");

public class CourierStateNotFoundException(Guid courierId)
    : BusinessException($"Courier state for courier id {courierId} not found.");
