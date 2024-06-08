using MediatR;
using VehicleRouting.Application.Core.Places;

namespace VehicleRouting.Api.Endpoints;

public static class DriverEndpoints
{
    public static IEndpointRouteBuilder MapDrivers(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/drivers")
            .WithGroupName("Drivers")
            .WithOpenApi();

        group.MapGet("", GetDrivers)
            .WithName("GetDrivers");

        return app;
    }

    private static async Task<IResult> GetDrivers(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetPlaces.Query(), cancellationToken);

        return TypedResults.Ok(result);
    }
}