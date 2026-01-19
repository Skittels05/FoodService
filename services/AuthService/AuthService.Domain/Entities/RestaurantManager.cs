namespace AuthService.Domain.Entities;

public class RestaurantManager : IEntityBase
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ManagedRestaurantId { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

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
}
