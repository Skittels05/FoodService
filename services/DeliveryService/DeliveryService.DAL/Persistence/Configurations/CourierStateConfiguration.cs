using DeliveryService.BLL.Constants;
using DeliveryService.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.DAL.Persistence.Configurations;

public class CourierStateConfiguration : IEntityTypeConfiguration<CourierState>
{
    public void Configure(EntityTypeBuilder<CourierState> builder)
    {
        builder.HasKey(c => c.CourierId);

        builder.Property(c => c.Status)
            .HasConversion<string>()
            .HasMaxLength(Constraints.CourierStatusMaxLength);
    }
}
