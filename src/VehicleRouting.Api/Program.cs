using Serilog;
using VehicleRouting.Api;
using VehicleRouting.Api.Endpoints;
using VehicleRouting.Application;
using VehicleRouting.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation()
    .AddApplication()
    .AddInfraestructure(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

// Endpoints
app
    .MapPlaces()
    .MapDrivers()
    .MapVehicles()
    .MapRouting();

await app.RunAsync();
