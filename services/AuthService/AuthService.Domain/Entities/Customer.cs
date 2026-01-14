namespace AuthService.Domain.Entities;

public class Customer
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string Phone { get; private set; }
    public string? Address { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected Customer() { }

    public Customer(Guid userId, string name, string phone, string? address = null)
    {
        Id = userId;
        UserId = userId;
        Name = name;
        Phone = phone;
        Address = address;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    public void UpdateAddress(string? address)
    {
        Address = address;
        UpdatedAt = DateTime.UtcNow;
    }
}

