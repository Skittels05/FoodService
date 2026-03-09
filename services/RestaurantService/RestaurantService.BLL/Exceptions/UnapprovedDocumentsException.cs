namespace RestaurantService.BLL.Exceptions;

public class UnapprovedDocumentsException : Exception
{
    public UnapprovedDocumentsException(Guid restaurantId)
        : base($"Cannot verify restaurant with Id '{restaurantId}' because not all documents are approved.")
    {
    }
}
