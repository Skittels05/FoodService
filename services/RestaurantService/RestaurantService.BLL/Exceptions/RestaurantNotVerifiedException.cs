namespace RestaurantService.BLL.Exceptions;

public class RestaurantNotVerifiedException : Exception
{
    public RestaurantNotVerifiedException(Guid restaurantId)
        : base($"Cannot activate restaurant with Id '{restaurantId}' because it is not verified yet.")
    {
    }
}
