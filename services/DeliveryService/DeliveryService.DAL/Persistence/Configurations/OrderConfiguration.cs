using DeliveryService.BLL.Constants;
using DeliveryService.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.DAL.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.TotalAmount)
            .HasPrecision(18, 2);

        builder.ComplexProperty(o => o.Delivery, address =>
        {
            address.Property(a => a.Address)
                .HasMaxLength(Constraints.AddressMaxLength)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.Pickup, address =>
        {
            address.Property(a => a.Address)
                .HasMaxLength(Constraints.AddressMaxLength)
                .IsRequired();
        });

        builder.Property(o => o.CustomerComment)
            .HasMaxLength(Constraints.OrderCustomerCommentMaxLength);

        builder.Property(o => o.CancellationComment)
            .HasMaxLength(Constraints.OrderCancellationCommentMaxLength);

        builder.Property(o => o.Status)
            .HasConversion<string>()
            .HasMaxLength(Constraints.OrderStatusMaxLength);

        builder.Property(o => o.CancellationReason)
            .HasConversion<string>()
            .HasMaxLength(Constraints.OrderCancellationReasonMaxLength);

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.Payments)
            .WithOne()
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
