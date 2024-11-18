using Api.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Configurations;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable("tb_coupon");

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Id)
           .IsRequired()
           .HasColumnName("id");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasColumnType("varchar(15)");

        builder.Property(c => c.Percent)
            .IsRequired()
            .HasColumnName("percent");

        builder.Property(c => c.IsActive)
            .IsRequired()
            .HasColumnName("active")
            .HasColumnType("bit")
            .HasDefaultValueSql("(1)");

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(c => c.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime");
    }
}