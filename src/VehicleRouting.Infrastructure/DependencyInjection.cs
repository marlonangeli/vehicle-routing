using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleRouting.Domain.Interfaces;
using VehicleRouting.Infrastructure.Database;
using VehicleRouting.Infrastructure.Repositories;

namespace VehicleRouting.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString(DatabaseConstants.VRPDatabase) ??
                   throw new ArgumentNullException("Connection string not found.",
                       $"{DatabaseConstants.VRPDatabase}");

        services.AddDbContext<VehicleRoutingDbContext>(options =>
        {
            options.UseNpgsql(conn,
                builder => builder.MigrationsAssembly(typeof(VehicleRoutingDbContext).Assembly.FullName)
                    .EnableRetryOnFailure());
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });

        services.AddScoped<IPlaceRepository, PlaceRepository>();
        // services.AddScoped<IDriverRepository, DriverRepository>();
        // services.AddScoped<IVehicleRepository, VehicleRepository>();

        return services;
    }
}