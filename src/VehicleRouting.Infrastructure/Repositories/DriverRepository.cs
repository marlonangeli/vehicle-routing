using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VehicleRouting.Domain.Entities;
using VehicleRouting.Domain.Interfaces;
using VehicleRouting.Infrastructure.Database;

namespace VehicleRouting.Infrastructure.Repositories;

public sealed class DriverRepository(VehicleRoutingDbContext context, ILogger<DriverRepository> logger) : IDriverRepository
{
    public async Task<Driver?> GetByIdAsync(Guid id)
    {
        logger.LogDebug("Getting driver with id {DriverId}", id);
        var result = await context.Drivers.AsNoTracking()
            .Include(i => i.WorkSchedules)
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

        return result;
    }

    public async Task<Driver> AddAsync(Driver entity)
    {
        try
        {
            logger.LogDebug("Adding new driver with name {DriverName}", entity.Name);

            var result = await context.Drivers.AddAsync(entity);

            logger.LogInformation("Driver with name {DriverName} added successfully", entity.Name);
            return result.Entity;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while adding driver with name {DriverName}", entity.Name);
            throw;
        }
    }

    public Task UpdateAsync(Driver entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Driver entity)
    {
        try
        {
            logger.LogDebug("Deleting driver with id {DriverId}", entity.Id);

            await context.Drivers
                .Where(x => x.Id == entity.Id)
                .ExecuteDeleteAsync();

            logger.LogInformation("Driver with id {DriverId} deleted successfully", entity.Id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while deleting driver with id {DriverId}", entity.Id);
            throw;
        }
    }

    public async Task<IReadOnlyList<Driver>> ListAllAsync()
    {
        logger.LogDebug("Getting all drivers");
        var result = await context.Drivers.AsNoTracking()
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