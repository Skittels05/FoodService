namespace RestaurantService.BLL.Exceptions;

public class RestaurantAlreadyVerifiedException(Guid restaurantId)
    : Exception($"Operation failed because restaurant with Id '{restaurantId}' is already verified.");

public class RestaurantNotVerifiedException(Guid restaurantId)
    : Exception($"Cannot activate restaurant with Id '{restaurantId}' because it is not verified yet.");

public class MissingRestaurantDocumentsException(Guid restaurantId)
    : Exception($"Restaurant with Id '{restaurantId}' has no uploaded documents for verification.");

public class UnapprovedDocumentsException(Guid restaurantId)
    : Exception($"Cannot verify restaurant with Id '{restaurantId}' because not all documents are approved.");
