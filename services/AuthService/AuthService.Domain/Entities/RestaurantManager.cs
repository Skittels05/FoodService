namespace AuthService.Domain.Entities;

public class RestaurantManager : EntityBase
{
    public Guid UserId { get; private set; }
    public Guid ManagedRestaurantId { get; private set; }
    public string Name { get; private set; }

    protected RestaurantManager() { }

    public RestaurantManager(
        Guid userId,
        Guid restaurantId,
        string name)
    {
        UserId = userId;
        ManagedRestaurantId = restaurantId;
        Name = name;
    }

    public void ChangeName(string newName)
    {
        Name = newName;
    }

    public void ChangeRestaurantId(Guid restaurantId)
    {
        ManagedRestaurantId = restaurantId;
    }
}
