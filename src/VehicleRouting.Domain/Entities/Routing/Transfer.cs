namespace VehicleRouting.Domain.Entities.Routing;

public class Transfer
{
    public Guid Id { get; set; }
    public int Pax { get; set; }
    public Place From { get; set; }
    public Place To { get; set; }
    public TimeOnly PickupTime { get; set; }
    public TimeOnly? ReturnTime { get; set; }
    public Vehicle Vehicle { get; set; }
    public Driver Driver { get; set; }
}