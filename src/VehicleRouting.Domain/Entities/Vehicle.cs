using VehicleRouting.Domain.Common;

namespace VehicleRouting.Domain.Entities;

public class Vehicle : Entity
{
    public string Plate { get; set; }
    public VehicleType Type { get; set; }
    public int Capacity { get; set; }
    public LicenseType License { get; set; }
    public bool HasInternationalLicence { get; set; }
    public double Consumption { get; set; }
    public FuelType FuelType { get; set; }
    public VehicleStatus Status { get; set; }
    public VehicleModel? Model { get; set; }
}

public enum VehicleType
{
    Car,
    Bus,
    Van
}

public enum VehicleStatus
{
    Available,
    Busy,
    Maintenance
}

public enum FuelType
{
    Gasoline,
    Diesel
}

public enum LicenseType
{
    A,
    B,
    C,
    D,
    E
}

public class VehicleModel
{
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
}