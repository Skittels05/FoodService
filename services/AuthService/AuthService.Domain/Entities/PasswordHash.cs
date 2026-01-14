namespace AuthService.Domain.Entities;

public class PasswordHash
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Hash { get; private set; }
    public string Salt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected PasswordHash() { }

    public PasswordHash(Guid userId, string hash, string salt)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Hash = hash;
        Salt = salt;
        CreatedAt = DateTime.UtcNow;
    }
}

