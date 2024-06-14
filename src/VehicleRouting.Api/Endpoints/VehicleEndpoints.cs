using FluentValidation;
using MediatR;
using VehicleRouting.Application.Core.Vehicles;

namespace VehicleRouting.Api.Endpoints;

public static class VehicleEndpoints
{
    public static IEndpointRouteBuilder MapVehicles(this IEndpointRouteBuilder app)
    {
        const string groupName = "Vehicles";
        var group = app.MapGroup("api/vehicles")
            .WithTags(groupName)
            .WithOpenApi();

        group.MapPost("", CreateVehicle)
            .WithName(nameof(CreateVehicle))
            .WithOpenApi();

        group.MapGet("", GetVehicles)
            .WithName(nameof(GetVehicles))
            .WithOpenApi();

        return app;
    }

    private static async Task<IResult> CreateVehicle(
        ISender sender,
        CreateVehicle.Command command,
        CancellationToken cancellationToken)
    {
        try
        {
            var id = await sender.Send(command, cancellationToken);

            return Results.Created($"api/vehicles/{id}", id);
        }
        catch (ValidationException e)
        {
            return Results.BadRequest(e.Errors);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    private static async Task<IResult> GetVehicles(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetVehicles.Query(), cancellationToken);

        return Results.Ok(result);
    }
}