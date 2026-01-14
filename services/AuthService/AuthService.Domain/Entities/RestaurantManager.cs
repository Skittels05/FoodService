namespace AuthService.Domain.Entities;

public class RestaurantManager
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ManagedRestaurantId { get; private set; }
    public string Name { get; private set; }
    public string Phone { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected RestaurantManager() { }

    public RestaurantManager(
        Guid userId,
        Guid restaurantId,
        string name,
        string phone)
    {
        Id = userId;
        UserId = userId;
        ManagedRestaurantId = restaurantId;
        Name = name;
        Phone = phone;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}

