namespace RestaurantService.BLL.Exceptions;

public class MissingRestaurantDocumentsException : Exception
{
    public MissingRestaurantDocumentsException(Guid restaurantId)
        : base($"Restaurant with Id '{restaurantId}' has no uploaded documents for verification.")
    {
    }
}
