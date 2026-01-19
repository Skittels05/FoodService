namespace AuthService.Domain.Entities;

public class CustomerAddress : IEntityBase
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public string Address { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime LastUsedAt { get; private set; }

    protected CustomerAddress() { }

    public CustomerAddress(Guid customerId, string address)
    {
        CustomerId = customerId;
        Address = address.Trim();
    }

    public void MarkAsUsed()
    {
        LastUsedAt = DateTime.UtcNow;
    }
}