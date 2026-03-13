using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantService.BLL.Models;

namespace RestaurantService.DAL.Configurations;

public sealed class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable("MenuItems");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Price).HasColumnType("decimal(18,2)");

        builder.HasOne<Restaurant>()
               .WithMany()
               .HasForeignKey(m => m.RestaurantId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
