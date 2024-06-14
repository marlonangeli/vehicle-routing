namespace VehicleRouting.Api.Endpoints;

public static class RoutingEndpoints
{
    public static IEndpointRouteBuilder MapRouting(this IEndpointRouteBuilder app)
    {
        const string groupName = "Routing";
        var group = app.MapGroup("api/routing")
            .WithTags(groupName)
            .WithOpenApi();

        group.MapGet("", () => Results.Ok("Roooutes"));

        return app;
    }
}