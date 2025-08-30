using Microsoft.AspNetCore.Mvc;
using WebMotors.Service.Interface;

namespace WebMotors.Endpoints;

public static class VehicleApi
{
    public static RouteGroupBuilder MapVehicleApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/vehicle");
        group.MapGet("", ([FromServices]IVehicleService vehicleService) =>
        {
            var test = vehicleService.GetVehicles();
            return Results.Ok(test);
        } ).WithName("GetVehicles");
        return group;
    }
    
}