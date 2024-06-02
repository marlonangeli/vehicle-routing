using VehicleRouting.Domain.Common;

namespace VehicleRouting.Domain.Entities;

public class WorkSchedule : Entity
{
    public Driver Driver { get; set; }
    public Guid DriverId { get; set; }
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }
    public DayOfWeek Day { get; set; }
}