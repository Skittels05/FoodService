namespace AuthService.Domain.Entities;

public class Customer : EntityBase
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }

    protected Customer() { }

    public Customer(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }
}
