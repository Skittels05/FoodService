using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantService.BLL.Models;

namespace RestaurantService.DAL.Configurations;

public sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("Locations");
        builder.HasKey(l => l.Id);

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
