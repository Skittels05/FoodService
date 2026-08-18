namespace DeliveryService.DAL.Exceptions;

public abstract class OutboxException : Exception
{
    protected OutboxException(string message, Exception? innerException = null) 
        : base(message, innerException)
    {
    }
}

public class OutboxTypeNotFoundException : OutboxException
{
    public OutboxTypeNotFoundException(string typeName)
        : base($"Type '{typeName}' could not be resolved.")
    {
    }
}

public class OutboxDeserializationException : OutboxException
{
    public OutboxDeserializationException(Guid messageId, string typeName)
        : base($"Failed to deserialize content of message '{messageId}' to type '{typeName}'.")
    {
    }
}
