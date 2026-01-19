namespace AuthService.Domain.Entities;

public class Customer : IEntityBase
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected Customer() { }

    public Customer(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }
}
