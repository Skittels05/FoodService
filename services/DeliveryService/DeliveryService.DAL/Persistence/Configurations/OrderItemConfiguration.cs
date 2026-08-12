namespace DeliveryService.DAL.Persistence.Configurations;

using DeliveryService.BLL.Constants;
using DeliveryService.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .HasMaxLength(Constraints.OrderItemNameMaxLength)
            .IsRequired();

        builder.Property(i => i.Price)
            .HasPrecision(18, 2);
    }
}
