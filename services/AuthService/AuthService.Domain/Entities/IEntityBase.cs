namespace AuthService.Domain.Entities;

public interface IEntityBase
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
    DateTime UpdatedAt { get; }
}
