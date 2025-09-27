namespace Domain.DTOs.Request;

public class BuyVehicleRequest 
{
    public Guid VehicleId { get; set; }
    public string UserAuth0Id { get; set; }
}