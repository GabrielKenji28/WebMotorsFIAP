using Infra.Models;
using WebMotors.Service.Interface;

namespace WebMotors.Service;

public class VehicleService : IVehicleService
{
    public List<Vehicle> GetVehicles()
    {
        var vehicle = new Vehicle()
        {
            Brand = "Volkswagen",
            Model = "Nivus",
            Color = "Red",
            Year = 2020,
            Price = 50000, 
        };
        
        return new List<Vehicle>() { vehicle };
    }
}