using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Persistence.Configurations;

public class RestaurantManagerConfiguration : IEntityTypeConfiguration<RestaurantManager>
{
    public void Configure(EntityTypeBuilder<RestaurantManager> builder)
    {
        builder.HasKey(rm => rm.Id);
        builder.HasIndex(rm => rm.UserId).IsUnique();
        builder.HasOne<User>()
               .WithOne()
               .HasForeignKey<RestaurantManager>(rm => rm.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.Property(rm => rm.Name)
            .IsRequired();
    }
}
