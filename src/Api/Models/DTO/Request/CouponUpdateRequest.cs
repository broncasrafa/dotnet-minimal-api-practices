namespace Api.Models.DTO.Request;

public class CouponUpdateRequest 
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public int Percent { get; set; }
}