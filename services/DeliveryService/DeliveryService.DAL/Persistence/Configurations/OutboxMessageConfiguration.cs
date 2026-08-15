using DeliveryService.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.DAL.Persistence.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Type)
            .IsRequired();

        builder.Property(m => m.Content)
            .HasColumnType("jsonb")
            .IsRequired();

        builder.HasIndex(m => new {m.OccurredOnUtc, m.Id})
            .HasFilter(@"""ProcessedOnUtc"" IS NULL")
            .HasDatabaseName("IX_OutboxMessages_Unprocessed");
    }
}
