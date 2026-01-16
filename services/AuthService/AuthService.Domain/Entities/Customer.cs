namespace AuthService.Domain.Entities;

public class Customer: EntityBase
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string Phone { get; private set; }
    public string? Address { get; private set; }

    protected Customer() { }

    public Customer(Guid userId, string name, string phone, string? address = null)
    {
        Id = userId;
        UserId = userId;
        Name = name;
        Phone = phone;
        Address = address;
    }
    public void UpdateAddress(string? address)
    {
        Address = address;
    }
}

