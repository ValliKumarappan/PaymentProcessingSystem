using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentProcessingSystem.SharedKernel.Domain;

namespace PaymentProcessingSystem.Infrastructure.Persistence.EntityConfigurations;
class PaymentConfig
       : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payment");
        builder.HasKey(cr => cr.Id);
        builder.Property(cr => cr.Amount).IsRequired();
        builder.Property(cr => cr.Currency).HasMaxLength(10).IsRequired();
        builder.Property(cr => cr.PaymentMethod).IsRequired();
        builder.Property(cr => cr.Status).IsRequired();
        builder.Property(cr => cr.TransactionDate).IsRequired();
        builder.HasIndex(a => new { a.Id });
    }
}