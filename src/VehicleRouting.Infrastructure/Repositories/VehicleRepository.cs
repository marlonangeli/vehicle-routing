using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VehicleRouting.Domain.Entities;
using VehicleRouting.Domain.Interfaces;
using VehicleRouting.Infrastructure.Database;

namespace VehicleRouting.Infrastructure.Repositories;

public sealed class VehicleRepository(VehicleRoutingDbContext context, ILogger<VehicleRepository> logger) : IVehicleRepository
{
    public async Task<Vehicle?> GetByIdAsync(Guid id)
    {
        logger.LogDebug("Getting vehicle with id {VehicleId}", id);
        var result = await context.Vehicles.AsNoTracking()
            .Include(i => i.Model)
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

        return result;
    }

    public async Task<Vehicle> AddAsync(Vehicle entity)
    {
        try
        {
            logger.LogDebug("Adding new vehicle with name {VehiclePlate}", entity.Plate);

            var result = await context.Vehicles.AddAsync(entity);

            logger.LogInformation("Vehicle with name {VehiclePlate} added successfully", entity.Plate);
            return result.Entity;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while adding vehicle with name {VehicleName}", entity.Plate);
            throw;
        }
    }

    public Task UpdateAsync(Vehicle entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Vehicle entity)
    {
        try
        {
            logger.LogDebug("Deleting vehicle with id {VehicleId}", entity.Id);

            await context.Vehicles
                .Where(x => x.Id == entity.Id)
                .ExecuteDeleteAsync();

            logger.LogInformation("Vehicle with id {VehicleId} deleted successfully", entity.Id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while deleting Vehicle with id {VehicleId}", entity.Id);
            throw;
        }
    }

    public async Task<IReadOnlyList<Vehicle>> ListAllAsync()
    {
        logger.LogDebug("Getting all Vehicles");
        var result = await context.Vehicles.AsNoTracking()
            .ToListAsync();

        return result;
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            logger.LogDebug("Saving changes to database");

            await context.SaveChangesAsync();

            logger.LogInformation("Changes saved successfully");
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while saving changes to database");
            throw;
        }
    }
}