using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleRouting.Application.Core.Places;

namespace VehicleRouting.Api.Endpoints;

public static class PlaceEndpoints
{
    public static IEndpointRouteBuilder MapPlaces(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/places")
            .WithTags("Places");

        group.MapGet("", GetPlaces)
            .WithName("GetPlaces")
            .WithOpenApi();

        group.MapPost("", CreatePlace)
            .WithName("CreatePlace")
            .WithOpenApi();

        return app;
    }

    private static async Task<IResult> GetPlaces(
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetPlaces.Query(), cancellationToken);

        return Results.Ok(result);
    }

    private static async Task<IResult> CreatePlace(
        [FromServices] ISender sender,
        CreatePlace.Command command,
        CancellationToken cancellationToken)
    {
        try
        {
            var id = await sender.Send(command, cancellationToken);

            return Results.Created($"/places/{id}", id);
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
}