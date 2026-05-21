namespace DeliveryService.BLL.Exceptions;

public abstract class DeliveryApplicationException(string message, Exception? innerException = null) 
    : Exception(message, innerException);
    
public class MappingException : DeliveryApplicationException
{
    public MappingException(Type sourceType, Type destinationType) 
        : base($"Execution failed during mapping from {sourceType.Name} to {destinationType.Name}. Source object was null.") { }

    public MappingException(Type sourceType, Type destinationType, bool isCollection) 
        : base($"Execution failed during mapping from PagedList<{sourceType.Name}> to PagedList<{destinationType.Name}>. Source paged list was null.") { }

    public MappingException(Type sourceType, Type destinationType, InvalidOperationException innerException) 
        : base($"Execution failed during mapping from {sourceType.Name} to {destinationType.Name}. Mapper is not registered in Dependency Injection.", innerException) { }
}
