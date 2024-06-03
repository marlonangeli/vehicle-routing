using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleRouting.Application.Core.CreateVehicle;

namespace VehicleRouting.Api.Endpoints;

public static class VehicleEndpoints
{
    public static IEndpointRouteBuilder MapVehicles(this IEndpointRouteBuilder route)
    {
        route.MapPost("/vehicles", CreateVehicle)
            .WithName("CreateVehicle")
            .WithOpenApi();

        return route;
    }

    public async static Task<IResult> CreateVehicle(
        [FromServices] ISender sender,
        [FromBody] CreateVehicle.Command command,
        CancellationToken cancellationToken)
    {
        var id = await sender.Send(command, cancellationToken);

        return Results.Created($"/vehicles/{id}", id);
    }
}