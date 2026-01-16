using AuthService.Domain.Enums;

namespace AuthService.Domain.Entities;

public class Courier: EntityBase
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string Phone { get; private set; }
    public VehicleType VehicleType { get; private set; }
    public string DocumentsPath { get; private set; }
    public string PhotoVerificationPath { get; private set; }
    public bool IsVerified { get; private set; }

    protected Courier() { }

    public Courier(
        Guid userId,
        string name,
        string phone,
        VehicleType vehicleType,
        string documentsPath,
        string photoVerificationPath)
    {
        UserId = userId;
        Name = name;
        Phone = phone;
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

