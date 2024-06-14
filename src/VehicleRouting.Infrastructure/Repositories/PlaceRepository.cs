using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VehicleRouting.Domain.Entities;
using VehicleRouting.Domain.Interfaces;
using VehicleRouting.Infrastructure.Database;

namespace VehicleRouting.Infrastructure.Repositories;

public sealed class PlaceRepository(VehicleRoutingDbContext context, ILogger<PlaceRepository> logger) : IPlaceRepository
{
    public async Task<Place?> GetByIdAsync(Guid id)
    {
        logger.LogDebug("Getting place with id {PlaceId}", id);
        var result = await context.Places.AsNoTracking()
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

        return result;
    }

    public async Task<Place> AddAsync(Place entity)
    {
        try
        {
            logger.LogDebug("Adding new place with name {PlaceName}", entity.Name);

            var result = await context.Places.AddAsync(entity);

            logger.LogInformation("Place with name {PlaceName} added successfully", entity.Name);
            return result.Entity;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while adding place with name {PlaceName}", entity.Name);
            throw;
        }
    }

    public Task UpdateAsync(Place entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Place entity)
    {
        try
        {
            logger.LogDebug("Deleting place with id {PlaceId}", entity.Id);

            await context.Places
                .Where(x => x.Id == entity.Id)
                .ExecuteDeleteAsync();

            logger.LogInformation("Place with id {PlaceId} deleted successfully", entity.Id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while deleting place with id {PlaceId}", entity.Id);
            throw;
        }
    }

    public async Task<IReadOnlyList<Place>> ListAllAsync()
    {
        logger.LogDebug("Getting all places");
        var result = await context.Places.AsNoTracking()
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