namespace RestaurantService.BLL.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entityName, Guid id)
        : base($"Entity '{entityName}' with Id '{id}' was not found.")
    {
    }
}
