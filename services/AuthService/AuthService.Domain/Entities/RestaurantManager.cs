namespace AuthService.Domain.Entities;

public class RestaurantManager: EntityBase
{
    public Guid UserId { get; private set; }
    public Guid ManagedRestaurantId { get; private set; }
    public string Name { get; private set; }
    public string Phone { get; private set; }

    protected RestaurantManager() { }

    public RestaurantManager(
        Guid userId,
        Guid restaurantId,
        string name,
        string phone)
    {
        UserId = userId;
        ManagedRestaurantId = restaurantId;
        Name = name;
        Phone = phone;
    }
}

