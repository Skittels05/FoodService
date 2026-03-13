using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantService.BLL.Models;

namespace RestaurantService.DAL.Configurations;

public sealed class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable("Restaurants");
        builder.HasKey(r => r.Id);
        builder.HasMany(r => r.Documents)
               .WithOne()
               .HasForeignKey(d => d.RestaurantId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
