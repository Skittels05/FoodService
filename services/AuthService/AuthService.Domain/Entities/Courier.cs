using AuthService.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Domain.Entities;

public class Courier : EntityBase
{
    public Guid UserId { get; private set; }
    [MaxLength(50)]
    public string Name { get; private set; }
    public VehicleType VehicleType { get; private set; }
    public string DocumentsPath { get; private set; }
    public string PhotoVerificationPath { get; private set; }
    public bool IsVerified { get; private set; }

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
    public void ChangeVehicle(VehicleType vehicleType)
    {
        VehicleType = vehicleType;
    }
    public void ChangeName(string name)
    {
        Name = name;
    }
}
