using System.Security.Claims;
using Domain.DTOs;
using Infra.Models;
using Infra.Repository.Interfaces;
using WebMotors.Service.Interface;

namespace Domain.Service;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _repository;
    private readonly IUserService _userService;
    public VehicleService(IVehicleRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    
    public async Task<IEnumerable<VehicleDTO>> GetAvailableVehiclesAsync()
    {
        var vehicles = await _repository.GetAllAvailableVehiclesAsync();
        return vehicles.Select(ToVehicleDto).ToList();
    }
    
    public async Task<IEnumerable<VehicleDTO>> GetSoldVehiclesAsync()
    {
        var vehicles = await _repository.GetAllSoldVehiclesAsync();
        return vehicles.Select(ToVehicleDto).ToList();
    }

    public async Task<bool> TryBuyVehicle(Guid vehicleId, string userId)
    {
        var vehicle = await _repository.GetById(vehicleId);
        if (vehicle == null) return false;
        
        var user = await _userService.CreateOrGetUserAsync(userId);
        if (user == null)
        {
            return false;
        }
        return await _repository.BuyVehicle(vehicle);
    }
    
    public IEnumerable<VehicleDTO> GetVehicles()
    {
        var vehicles = _repository.GetAll();
        return vehicles.Select(ToVehicleDto).ToList();
    }

    public bool AddVehicle(VehicleDTO vehicleDto)
    {
        var user = _userService.CreateOrGetUserAsync(vehicleDto.UserAuth0Id).Result;
        vehicleDto.Id = Guid.NewGuid();        
        var vehicleToAdd = ToVehicle(vehicleDto);
        vehicleToAdd.CreatedAt = DateTime.Now;
        vehicleToAdd.UpdatedAt = DateTime.Now;
        vehicleToAdd.IsActive = true;
        vehicleToAdd.IsAvailableToBuy = true;
        return _repository.TryAddVehicle(vehicleToAdd);
    }
    
    public async Task<bool> TryUpdateVehicle(VehicleDTO vehicleDto)
    {
        var user = await _userService.CreateOrGetUserAsync(vehicleDto.UserAuth0Id);
        if (user == null)
        {
            return false;
        }
        var vehicleToAdd = ToVehicle(vehicleDto);
        vehicleToAdd.UpdatedAt = DateTime.Now;
        vehicleToAdd.IsActive = true;

        return await _repository.TryUpdateVehicle(vehicleToAdd);
    }

    private static Vehicle ToVehicle(VehicleDTO vehicleDto)
    {
        return new Vehicle()
        {
            Id = vehicleDto.Id,
            Brand = vehicleDto.Brand,
            Model = vehicleDto.Model,
            Year = vehicleDto.Year,
            Color = vehicleDto.Color,
            Price = vehicleDto.Price,
            UserAuth0Id = vehicleDto.UserAuth0Id,
            
        };
    }

    private static VehicleDTO ToVehicleDto(Vehicle vehicle)
    {
        return new VehicleDTO()
        {
            Brand = vehicle.Brand,
            Model = vehicle.Model,
            Color = vehicle.Color,
            Year = vehicle.Year,
            Price = vehicle.Price,
            Id = vehicle.Id
        };
    }
}