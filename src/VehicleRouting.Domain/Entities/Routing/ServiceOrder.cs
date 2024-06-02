namespace VehicleRouting.Domain.Entities.Routing;

public class ServiceOrder
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Transfer> Transfers { get; set; }
    public TimeOnly StartTime => Transfers.Min(t => t.PickupTime);
    public TimeOnly ReturnTime { get; set; }

    public int TotalPax => Transfers.Sum(t => t.Pax);
    public bool HasTourGuide { get; set; }

}