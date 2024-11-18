using Api.Data.Configurations;
using Api.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.SetCommandTimeout(TimeSpan.FromSeconds(60));
    }

    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<LocalUser> LocalUsers { get; set; }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new CouponConfiguration());
        builder.ApplyConfiguration(new LocalUserConfiguration());
    }
}
