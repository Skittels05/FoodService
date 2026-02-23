using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasIndex(u => u.Auth0Id).IsUnique();
        builder.Property(u => u.Auth0Id).IsRequired();

        builder.Property(u => u.Email).IsRequired();
        builder.Property(u => u.UserName).IsRequired();

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .IsRequired();
    }
}
