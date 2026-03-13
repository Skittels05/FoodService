using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantService.BLL.Models;

namespace RestaurantService.DAL.Configurations;

public sealed class RestaurantDocumentConfiguration : IEntityTypeConfiguration<RestaurantDocument>
{
    public void Configure(EntityTypeBuilder<RestaurantDocument> builder)
    {
        builder.ToTable("RestaurantDocuments");
        builder.HasKey(d => d.Id);
    }
}
