namespace Infra.Models;

public class BaseModelInfra : BaseModel
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    
}