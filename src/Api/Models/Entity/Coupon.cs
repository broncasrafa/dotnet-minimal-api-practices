using Api.Data.Seed;

namespace Api.Models.Entity;

public class Coupon 
{
    public int Id { get; set; }
    public string Name { get; set;}
    public int Percent { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}