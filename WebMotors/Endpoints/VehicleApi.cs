namespace WebMotors.Endpoints;

public static class VehicleApi
{
    public static RouteGroupBuilder MapVehicleApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/vehicle");
        group.MapGet("", (IVehicleService vehicleService) =>
        {
            var test = vehicleService.GetVehicle()
        } )
    }
    
}