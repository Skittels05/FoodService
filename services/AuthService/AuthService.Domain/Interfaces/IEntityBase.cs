namespace AuthService.Domain.Interfaces;

public interface IEntityBase
{
    Guid Id { get; init; }
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}
