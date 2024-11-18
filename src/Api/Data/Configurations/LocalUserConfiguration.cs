using Api.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Configurations;

public class LocalUserConfiguration : IEntityTypeConfiguration<LocalUser>
{
    public void Configure(EntityTypeBuilder<LocalUser> builder)
    {
        builder.ToTable("tb_local_user");

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Id)
           .IsRequired()
           .HasColumnName("id");

        builder.Property(c => c.Username)
            .IsRequired()
            .HasColumnName("username")
            .HasColumnType("varchar(150)");
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasColumnType("varchar(150)");

        builder.Property(c => c.Password)
            .IsRequired()
            .HasColumnName("password")
            .HasColumnType("text");
        
        builder.Property(c => c.Role)
            .IsRequired()
            .HasColumnName("role")
            .HasColumnType("varchar(150)");
    }
}

