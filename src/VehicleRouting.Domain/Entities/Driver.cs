using VehicleRouting.Domain.Common;

namespace VehicleRouting.Domain.Entities;

public class Driver : Entity
{
    public string Name { get; set; }
    public LicenseType License { get; set; }
    public DriverStatus Status { get; set; }
    public List<WorkSchedule> WorkSchedules { get; set; }
}

public enum DriverStatus
{
    Available,
    Busy,
    NotAvailable
}