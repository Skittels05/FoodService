namespace AuthService.Domain.Entities;

public class RecoveryToken
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime? UsedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected RecoveryToken() { }

    public RecoveryToken(Guid userId, string token, DateTime expiresAt)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsUsed()
    {
        UsedAt = DateTime.UtcNow;
    }

    public bool IsValid =>
        UsedAt == null && DateTime.UtcNow < ExpiresAt;
}

