using System.Security.Claims;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebMotors.Service.Interface;

namespace WebMotors.Endpoints;

public static class VehicleApi
{
    public static RouteGroupBuilder MapVehicleApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/vehicle");
        group.MapGet("/available", async ([FromServices]IVehicleService vehicleService) =>
        {
            var vehicles = await vehicleService.GetAvailableVehiclesAsync();
            return Results.Ok(vehicles);
        } ).RequireAuthorization().WithName("GetAvailableVehicles");
        
        group.MapGet("/sold", async ([FromServices]IVehicleService vehicleService) =>
        {
            var vehicles = await vehicleService.GetSoldVehiclesAsync();
            return Results.Ok(vehicles);
        } ).RequireAuthorization().WithName("GetSoldVehicles");
        
        group.MapPost("/buy/{vehicleId}", async ([FromServices]IVehicleService vehicleService, HttpContext httpContext, Guid vehicleId) =>
        {
            var auth0UserId = httpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(auth0UserId) ){ return Results.Unauthorized(); }

            var vehicles = await vehicleService.TryBuyVehicle(vehicleId, auth0UserId);
            return Results.Ok(vehicles);
        } ).RequireAuthorization().WithName("BuyVehicle");
        
        group.MapPost("",  ([FromServices] IVehicleService vehicleService, VehicleDTO vehicleDto, HttpContext httpContext) =>
        {
            var auth0UserId = httpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(auth0UserId) ){ return Results.Unauthorized(); }

            vehicleDto.UserAuth0Id = auth0UserId;
            var success = vehicleService.AddVehicle(vehicleDto);
            return success ? Results.Ok(vehicleDto) : Results.BadRequest();
        }).RequireAuthorization().WithName("AddVehicle");
        
        group.MapPost("/update", async ([FromServices] IVehicleService vehicleService, VehicleDTO vehicleDto, HttpContext httpContext) =>
        {
            var auth0UserId = httpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(auth0UserId) ){ return Results.Unauthorized(); }

            vehicleDto.UserAuth0Id = auth0UserId;
            var success = await vehicleService.TryUpdateVehicle(vehicleDto);
            return success ? Results.Ok(vehicleDto) : Results.BadRequest();
        }).RequireAuthorization().WithName("UpdateVehicle");
        
        return group;
    }
    
}