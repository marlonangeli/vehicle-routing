using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using VehicleRouting.Application.Core.Drivers;

namespace VehicleRouting.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            // Generated schemaIds must be `Feature.Command` instead of `Feature+Command`
            options.CustomSchemaIds(type =>
            {
                var name = Enumerable.Last(type.FullName!.Split('.'));
                if (name.Contains('+'))
                    name = name.Replace('+', '.');

                return name;
            });

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Vehicle Routing API",
                Version = "v1"
            });
        });

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        return services;
    }
}