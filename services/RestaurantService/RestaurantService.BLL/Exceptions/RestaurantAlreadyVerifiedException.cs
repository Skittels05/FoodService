namespace RestaurantService.BLL.Exceptions;

public class RestaurantAlreadyVerifiedException : Exception
{
    public RestaurantAlreadyVerifiedException(Guid restaurantId)
        : base($"Operation failed because restaurant with Id '{restaurantId}' is already verified.")
    {
    }
}
