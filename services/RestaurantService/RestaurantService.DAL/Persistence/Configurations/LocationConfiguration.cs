using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantService.BLL.Constants;
using RestaurantService.BLL.Models;

namespace RestaurantService.DAL.Configurations;

public sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("Locations");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Address)
               .IsRequired()
               .HasMaxLength(ValidationConstants.LocationAddressMaxLength);

        builder.Property(l => l.Latitude).IsRequired();
        builder.Property(l => l.Longitude).IsRequired();
        builder.Property(l => l.IsAcceptingOrders).IsRequired();
        builder.HasIndex(l => new { l.RestaurantId, l.Address }).IsUnique();

        builder.HasOne<Restaurant>()
               .WithMany()
               .HasForeignKey(l => l.RestaurantId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(l => l.StopList)
               .WithOne()
               .HasForeignKey(s => s.LocationId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
