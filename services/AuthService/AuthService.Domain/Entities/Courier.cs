using AuthService.Domain.Enums;

namespace AuthService.Domain.Entities;

public class Courier : IEntityBase
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
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
        VehicleType vehicleType,
        string documentsPath,
        string photoVerificationPath)
    {
        UserId = userId;
        Name = name;
        VehicleType = vehicleType;
        DocumentsPath = documentsPath;
        PhotoVerificationPath = photoVerificationPath;
        IsVerified = false;
    }

    public void Verify()
    {
        IsVerified = true;
    }
}