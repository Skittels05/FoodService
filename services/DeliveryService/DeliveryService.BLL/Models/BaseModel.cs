namespace DeliveryService.BLL.Models;

public abstract class BaseModel
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }
}

