using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleRouting.Infrastructure.Database;

namespace VehicleRouting.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<VehicleRoutingDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(DatabaseConstants.VRPDatabase),
                builder => builder.MigrationsAssembly(typeof(VehicleRoutingDbContext).Assembly.FullName));
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });

        return services;
    }
}