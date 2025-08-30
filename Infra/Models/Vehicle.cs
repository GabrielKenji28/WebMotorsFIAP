namespace Infra.Models;

public class Vehicle : BaseModel
{
    public string Model { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
}