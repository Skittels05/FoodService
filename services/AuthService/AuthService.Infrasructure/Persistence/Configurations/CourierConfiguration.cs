using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Persistence.Configurations;

public class CourierConfiguration : IEntityTypeConfiguration<Courier>
{
    public void Configure(EntityTypeBuilder<Courier> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.UserId).IsUnique();
        builder.HasOne<User>()
               .WithOne()
               .HasForeignKey<Courier>(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(c => c.VehicleType)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(c => c.DocumentsPath).IsRequired();
        builder.Property(c => c.PhotoVerificationPath).IsRequired();
    }
}
