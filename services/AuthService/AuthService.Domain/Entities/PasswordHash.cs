namespace AuthService.Domain.Entities;

public class PasswordHash: EntityBase
{
    public Guid UserId { get; private set; }
    public string Hash { get; private set; }
    public string Salt { get; private set; }

    protected PasswordHash() { }

    public PasswordHash(Guid userId, string hash, string salt)
    {;
        UserId = userId;
        Hash = hash;
        Salt = salt;
    }
}

