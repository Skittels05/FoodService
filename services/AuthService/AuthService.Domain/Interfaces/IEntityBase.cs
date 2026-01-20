namespace AuthService.Domain.Interfaces;

public interface IEntityBase
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
}
