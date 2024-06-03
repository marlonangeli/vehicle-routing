using VehicleRouting.Application.Common.Messaging;
using VehicleRouting.Domain.Entities;

namespace VehicleRouting.Application.Core.CreateVehicle;

public static class CreateVehicle
{
    public record Command(string Plate,
        VehicleType Type,
        int Capacity,
        LicenseType License,
        bool HasInternationalLicense,
        double Consumption,
        FuelType FuelType) : ICommand<Guid>;

    public class Handler : ICommandHandler<Command, Guid>
    {
        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
        {
            var vehicle = new Vehicle
            {
                Plate = request.Plate,
                Type = request.Type,
                Capacity = request.Capacity,
                License = request.License,
                HasInternationalLicence = request.HasInternationalLicense,
                Consumption = request.Consumption,
                FuelType = request.FuelType,
                Status = VehicleStatus.Available
            };

            // Save vehicle to database

            return vehicle.Id;
        }
    }
}