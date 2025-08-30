using Infra.Models;

namespace WebMotors.Service.Interface;

public interface IVehicleService
{
    List<Vehicle> GetVehicles();
}