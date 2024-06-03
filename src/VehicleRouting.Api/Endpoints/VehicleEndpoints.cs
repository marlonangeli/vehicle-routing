using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleRouting.Application.Core.Vehicles;

namespace VehicleRouting.Api.Endpoints;

public static class VehicleEndpoints
{
    public static IEndpointRouteBuilder MapVehicles(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/vehicles")
            .WithGroupName("Vehicles")
            .WithOpenApi();

        group.MapPost("", CreateVehicle)
            .WithName("CreateVehicle")
            .WithOpenApi();

        return app;
    }

    private static async Task<IResult> CreateVehicle(
        [FromServices] ISender sender,
        [FromBody] CreateVehicle.Command command,
        CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);

        return Results.Created($"/vehicles/{id}", id);
    }
}