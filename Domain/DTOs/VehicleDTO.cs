using Infra.Models;

namespace Domain.DTOs;

public class VehicleDTO : BaseModel
{
    public string Model { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public string UserAuth0Id { get; set; }
}