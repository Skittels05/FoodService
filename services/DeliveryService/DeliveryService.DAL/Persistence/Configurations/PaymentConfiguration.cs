namespace DeliveryService.DAL.Persistence.Configurations;
    
using DeliveryService.BLL.Constants;
using DeliveryService.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
            .HasPrecision(18, 2);

        builder.Property(p => p.Status)
            .HasConversion<string>()
            .HasMaxLength(Constraints.PaymentStatusMaxLength);

        builder.Property(p => p.Method)
            .HasConversion<string>()
            .HasMaxLength(Constraints.PaymentMethodMaxLength);

        builder.Property(p => p.ExternalTransactionId)
            .HasMaxLength(Constraints.PaymentExternalTransactionIdMaxLength);

        builder.Property(p => p.PaymentProvider)
            .HasMaxLength(Constraints.PaymentProviderMaxLength);

        builder.Property(p => p.ErrorMessage)
            .HasMaxLength(Constraints.PaymentErrorMessageMaxLength);
    }
}
