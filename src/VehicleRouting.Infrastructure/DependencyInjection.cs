using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleRouting.Infrastructure.Database;

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
            options.UseNpgsql(configuration.GetConnectionString(conn),
                builder => builder.MigrationsAssembly(typeof(VehicleRoutingDbContext).Assembly.FullName));
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });

        return services;
    }
}