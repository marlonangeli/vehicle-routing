using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleRouting.Application.Core.Places;

namespace VehicleRouting.Api.Endpoints;

public static class PlaceEndpoints
{
    public static IEndpointRouteBuilder MapPlaces(this IEndpointRouteBuilder app)
    {
        const string groupName = "Places";
        var group = app.MapGroup("api/places")
            .WithTags(groupName);

        group.MapPost("", CreatePlace)
            .WithName(nameof(CreatePlace))
            .WithOpenApi();

        group.MapGet("", GetPlaces)
            .WithName(nameof(GetPlaces))
            .WithOpenApi();

        return app;
    }

    private static async Task<IResult> CreatePlace(
        ISender sender,
        CreatePlace.Command command,
        CancellationToken cancellationToken)
    {
        try
        {
            var id = await sender.Send(command, cancellationToken);

            return Results.Created($"api/places/{id}", id);
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

    private static async Task<IResult> GetPlaces(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetPlaces.Query(), cancellationToken);

        return Results.Ok(result);
    }
}