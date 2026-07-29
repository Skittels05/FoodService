using DeliveryService.BLL.Enums;

namespace DeliveryService.BLL.Exceptions;

public abstract class BusinessException(string message) : Exception(message);

public class NotFoundException(string name, Guid id) 
    : BusinessException($"{name} with id {id} not found.");

public class CourierStateNotFoundException(Guid courierId)
    : BusinessException($"Courier state for courier id {courierId} not found.");

public class InvalidOrderStateException(Guid orderId, OrderStatus currentStatus, OrderStatus expectedStatus)
    : BusinessException($"Order {orderId} is in status '{currentStatus}', but expected '{expectedStatus}'.");

public class OrderCourierMismatchException(Guid orderId)
    : BusinessException($"Order {orderId} is already assigned to a different courier.");

public class OrderFinalizedException(Guid orderId, OrderStatus currentStatus)
    : BusinessException($"Cannot assign courier to order {orderId} because it is already finalized (status: {currentStatus}).");

public class OrderCannotBeCancelledException(Guid orderId, OrderStatus currentStatus)
    : BusinessException($"Order {orderId} in status '{currentStatus}' cannot be cancelled.");
