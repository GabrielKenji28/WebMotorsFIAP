using Infra.DataContext;
using Infra.Models;
using Infra.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;

public class VehicleRepository : IVehicleRepository
{
    private readonly AppDbContext _context;

    public VehicleRepository(AppDbContext context)
    {
        _context = context; 
    }

    public List<Vehicle> GetAll()
    {
        return _context.Vehicles.ToList();
    }

    public async Task <bool> TryUpdateVehicle(Vehicle vehicle)
    {
        try
        {
            var vehicleEntity = await _context.Vehicles.FindAsync(vehicle.Id);
            if (vehicleEntity == null){ return false; }
            
            var isSoldOriginal = vehicleEntity.IsSold;
            var isAvailableToBuyOriginal = vehicleEntity.IsAvailableToBuy;

            _context.Entry(vehicleEntity).CurrentValues.SetValues(vehicle);

            vehicleEntity.IsSold = isSoldOriginal;
            vehicleEntity.IsAvailableToBuy = isAvailableToBuyOriginal;
            
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;    
        }
    }
    
    public bool TryAddVehicle(Vehicle vehicle)
    {
        try
        {
            _context.Vehicles.Add(vehicle);
            
            return _context.SaveChanges() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
           return false;    
        }

    }

    public async Task<Vehicle?> GetById(Guid id)
    {
        return await _context.Vehicles.FindAsync(id);
    }

    public async Task<bool> BuyVehicle(Vehicle vehicle)
    {
        vehicle.IsSold = true;
        vehicle.IsAvailableToBuy = false;
        vehicle.UpdatedAt = DateTime.Now;
        _context.Vehicles.Update(vehicle);
        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<IEnumerable<Vehicle>> GetAllAvailableVehiclesAsync()
    {
        var vehicles = await _context.Vehicles
            .AsNoTracking()
            .Where(v => v.IsActive  && v.IsAvailableToBuy )
            .OrderBy(v => v.Price )
            .ToListAsync();

        return vehicles;
    }    
    public async Task<IEnumerable<Vehicle>> GetAllSoldVehiclesAsync()
    {
        var vehicles = await _context.Vehicles
            .AsNoTracking()
            .Where(v => v.IsActive  && v.IsSold )
            .OrderBy(v => v.Price )
            .ToListAsync();

        return vehicles;
    }

}