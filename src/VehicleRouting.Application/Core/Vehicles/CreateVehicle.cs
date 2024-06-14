using FluentValidation;
using VehicleRouting.Application.Common.Messaging;
using VehicleRouting.Domain.Constants;
using VehicleRouting.Domain.Entities;
using VehicleRouting.Domain.Interfaces;

namespace VehicleRouting.Application.Core.Vehicles;

public static class CreateVehicle
{
    public sealed record Command(
        string Plate,
        VehicleType Type,
        int Capacity,
        LicenseType License,
        bool HasInternationalLicense,
        double Consumption,
        FuelType FuelType) : ICommand<Guid>;

    internal sealed class Handler(IVehicleRepository repository) : ICommandHandler<Command, Guid>
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

            await repository.AddAsync(vehicle);
            await repository.SaveChangesAsync();

            return vehicle.Id;
        }
    }

    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Plate)
                .NotEmpty()
                .NotNull()
                .MaximumLength(EntityConstants.Vehicle.PlateMaxLength);

            RuleFor(x => x.Type)
                .IsInEnum();

            RuleFor(x => x.Capacity)
                .GreaterThan(0);

            RuleFor(x => x.License)
                .IsInEnum();

            RuleFor(x => x.Consumption)
                .GreaterThan(0);

            RuleFor(x => x.FuelType)
                .IsInEnum();
        }
    }
}