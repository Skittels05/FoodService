using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantService.BLL.Constants;
using RestaurantService.BLL.Models;

namespace RestaurantService.DAL.Configurations;

public sealed class RestaurantDocumentConfiguration : IEntityTypeConfiguration<RestaurantDocument>
{
    public void Configure(EntityTypeBuilder<RestaurantDocument> builder)
    {
        builder.ToTable("RestaurantDocuments");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.FileUrl)
               .IsRequired()
               .HasMaxLength(ValidationConstants.DocumentFileUrlMaxLength);

        builder.Property(d => d.Type).IsRequired();
        builder.Property(d => d.Status).IsRequired();

        builder.Property(d => d.RejectionReason)
               .HasMaxLength(ValidationConstants.DocumentRejectionReasonMaxLength);
    }
}
