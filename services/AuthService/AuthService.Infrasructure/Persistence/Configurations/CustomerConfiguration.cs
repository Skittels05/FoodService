using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.UserId).IsUnique();
        builder.HasOne<User>()
               .WithOne()
               .HasForeignKey<Customer>(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasMany<CustomerAddress>()
               .WithOne()
               .HasForeignKey(a => a.CustomerId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
