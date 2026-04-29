namespace RestaurantService.BLL.Exceptions;

public class NotFoundException(string entityName, Guid id)
    : Exception($"Entity '{entityName}' with Id '{id}' was not found.");

public class MappingException(Type sourceType, Type destinationType)
    : Exception($"Cannot map from '{sourceType.Name}' to '{destinationType.Name}' because the source object is null.");

public class AccessDeniedException()
    : Exception($"You don't have access for this resource");

