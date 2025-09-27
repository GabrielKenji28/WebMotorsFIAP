using Infra.Models;

namespace Infra.Repository.Interfaces;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAllAvailableVehiclesAsync();
    Task<IEnumerable<Vehicle>> GetAllSoldVehiclesAsync();
    Task<bool> BuyVehicle(Vehicle vehicle);
    List<Vehicle> GetAll();
    bool TryAddVehicle(Vehicle vehicle);
    Task<Vehicle?> GetById(Guid id);
    Task<bool> TryUpdateVehicle(Vehicle vehicle);
}