using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantService.BLL.Models;

namespace RestaurantService.DAL.Configurations;

public sealed class StopListItemConfiguration : IEntityTypeConfiguration<StopListItem>
{
    public void Configure(EntityTypeBuilder<StopListItem> builder)
    {
        builder.ToTable("StopListItems");
        builder.HasKey(s => s.Id);

        builder.HasOne<MenuItem>()
               .WithMany()
               .HasForeignKey(s => s.MenuItemId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
