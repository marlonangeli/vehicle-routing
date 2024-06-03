namespace VehicleRouting.Api.Endpoints;

public static class RoutingEndpoints
{
    public static IEndpointRouteBuilder MapRouting(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/routing")
            .WithGroupName("Routing")
            .WithOpenApi();

        group.MapGet("", () => Results.Ok("Roooutes"));

        return app;
    }
}