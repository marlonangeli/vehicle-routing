using VehicleRouting.Domain.Common;

namespace VehicleRouting.Domain.Entities;

public class Place : Entity
{
    public Address Address { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}