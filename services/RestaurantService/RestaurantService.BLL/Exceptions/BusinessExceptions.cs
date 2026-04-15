namespace RestaurantService.BLL.Exceptions;

public abstract class BusinessRuleException(string message) : Exception(message);
public class RestaurantAlreadyVerifiedException(Guid restaurantId)
    : BusinessRuleException($"Operation failed because restaurant with Id '{restaurantId}' is already verified.");

public class RestaurantNotVerifiedException(Guid restaurantId)
    : BusinessRuleException($"Cannot activate restaurant with Id '{restaurantId}' because it is not verified yet.");

public class MissingRestaurantDocumentsException(Guid restaurantId)
    : BusinessRuleException($"Restaurant with Id '{restaurantId}' has no uploaded documents for verification.");

public class UnapprovedDocumentsException(Guid restaurantId)
    : BusinessRuleException($"Cannot verify restaurant with Id '{restaurantId}' because not all documents are approved.");
