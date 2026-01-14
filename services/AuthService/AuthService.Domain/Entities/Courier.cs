using AuthService.Domain.Enums;

namespace AuthService.Domain.Entities;

public class Courier
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string Phone { get; private set; }
    public VehicleType VehicleType { get; private set; }
    public string DocumentsPath { get; private set; }
    public string PhotoVerificationPath { get; private set; }
    public bool IsVerified { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected Courier() { }

    public Courier(
        Guid userId,
        string name,
        string phone,
        VehicleType vehicleType,
        string documentsPath,
        string photoVerificationPath)
    {
        Id = userId;
        UserId = userId;
        Name = name;
        Phone = phone;
        VehicleType = vehicleType;
        DocumentsPath = documentsPath;
        PhotoVerificationPath = photoVerificationPath;
        IsVerified = false;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Verify()
    {
        IsVerified = true;
        UpdatedAt = DateTime.UtcNow;
    }
}

