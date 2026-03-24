using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantService.BLL.Constants;
using RestaurantService.BLL.Models;

namespace RestaurantService.DAL.Configurations;

public sealed class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable("MenuItems");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
               .IsRequired()
               .HasMaxLength(ValidationConstants.MenuItemNameMaxLength);

        builder.Property(m => m.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(m => m.IsActive).IsRequired();

        builder.HasIndex(m => new { m.RestaurantId, m.Name }).IsUnique();

        builder.HasOne<Restaurant>()
               .WithMany()
               .HasForeignKey(m => m.RestaurantId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
