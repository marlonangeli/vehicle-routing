namespace VehicleRouting.Api.Endpoints;

public static class PlacesEndpoints
{
    public static IEndpointRouteBuilder MapPlaces(this IEndpointRouteBuilder route)
    {
        route.MapGet("/places", GetPlaces)
            .WithName("GetPlaces")
            .WithOpenApi();

        return route;
    }

    public async static Task<IResult> GetPlaces()
    {
        return Results.Ok("Oooh, places!");
    }
}