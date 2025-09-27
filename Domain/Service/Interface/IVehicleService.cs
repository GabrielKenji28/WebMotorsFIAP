using Domain.DTOs;
using Infra.Models;

namespace WebMotors.Service.Interface;

public interface IVehicleService
{
    Task<IEnumerable<VehicleDTO>> GetAvailableVehiclesAsync();
    IEnumerable<VehicleDTO> GetVehicles();
    bool AddVehicle(VehicleDTO vehicleDto);
    Task<IEnumerable<VehicleDTO>> GetSoldVehiclesAsync();
    Task<bool> TryBuyVehicle(Guid vehicleId, string userId);
    Task<bool> TryUpdateVehicle(VehicleDTO vehicleDto);
}