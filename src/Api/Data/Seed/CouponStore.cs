using Api.Models.Entity;

namespace Api.Data.Seed;

public class CouponStore
{
    private static List<Coupon> _coupons = new List<Coupon> 
    {
        new Coupon { Id = 1, Name = "BLACK10",   Percent = 10, IsActive = true, CreatedAt = DateTime.UtcNow },
        new Coupon { Id = 2, Name = "SUMMER15",  Percent = 15, IsActive = true, CreatedAt = DateTime.UtcNow },
        new Coupon { Id = 3, Name = "WINTER20",  Percent = 20, IsActive = false, CreatedAt = DateTime.UtcNow.AddDays(-30), UpdatedAt = DateTime.UtcNow.AddDays(-15) },
        new Coupon { Id = 4, Name = "SPRING25",  Percent = 25, IsActive = true, CreatedAt = DateTime.UtcNow },
        new Coupon { Id = 5, Name = "AUTUMN30",  Percent = 30, IsActive = false, CreatedAt = DateTime.UtcNow.AddDays(-60), UpdatedAt = DateTime.UtcNow.AddDays(-30) },
        new Coupon { Id = 6, Name = "HOLIDAY50", Percent = 50, IsActive = true, CreatedAt = DateTime.UtcNow },
        new Coupon { Id = 7, Name = "FLASH5",    Percent = 5, IsActive = false, CreatedAt = DateTime.UtcNow.AddDays(-10), UpdatedAt = DateTime.UtcNow.AddDays(-5) },
        new Coupon { Id = 8, Name = "FREESHIP",  Percent = 100, IsActive = true, CreatedAt = DateTime.UtcNow },
        new Coupon { Id = 9, Name = "CYBERMONDAY40", Percent = 40, IsActive = false, CreatedAt = DateTime.UtcNow.AddDays(-20), UpdatedAt = DateTime.UtcNow.AddDays(-10) },
        new Coupon { Id = 10, Name = "WELCOME15", Percent = 15, IsActive = true, CreatedAt = DateTime.UtcNow }
    };


    private static int SetNewId() 
    {
        var lastId = _coupons.Max(x => x.Id);
        return lastId+1;
    }

    public static List<Coupon> GetCoupons() => _coupons;
    public static Coupon CreateCoupon(Coupon newCoupon) 
    {
        newCoupon.Id = SetNewId();
        _coupons.Add(newCoupon);
        return newCoupon;
    }
    
}