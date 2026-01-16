namespace AuthService.Domain.Entities;

public class RecoveryToken: EntityBase
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime? UsedAt { get; private set; }

    protected RecoveryToken() { }

    public RecoveryToken(Guid userId, string token, DateTime expiresAt)
    {
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
    }

    public void MarkAsUsed()
    {
        UsedAt = DateTime.UtcNow;
    }

    public bool IsValid =>
        UsedAt == null && DateTime.UtcNow < ExpiresAt;
}

