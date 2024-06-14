using FluentValidation;
using MediatR;
using VehicleRouting.Application.Core.Drivers;

namespace VehicleRouting.Api.Endpoints;

public static class DriverEndpoints
{
    public static IEndpointRouteBuilder MapDrivers(this IEndpointRouteBuilder app)
    {
        const string groupName = "Drivers";
        var group = app.MapGroup("api/drivers")
            .WithTags(groupName)
            .WithOpenApi();

        group.MapPost("", CreateDriver)
            .WithName(nameof(CreateDriver))
            .WithOpenApi();

        group.MapGet("", GetDrivers)
            .WithName(nameof(GetDrivers))
            .WithOpenApi();

        return app;
    }

    private static async Task<IResult> CreateDriver(
        ISender sender,
        CreateDriver.Command command,
        CancellationToken cancellationToken)
    {
        try
        {
            var id = await sender.Send(command, cancellationToken);

            return Results.Created($"api/drivers/{id}", id);
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

    private static async Task<IResult> GetDrivers(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetDrivers.Query(), cancellationToken);

        return TypedResults.Ok(result);
    }
}