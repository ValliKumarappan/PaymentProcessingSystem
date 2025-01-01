using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentProcessingSystem.SharedKernel.Domain;

namespace PaymentProcessingSystem.Infrastructure.Persistence.EntityConfigurations;

class CustomerConfig
       : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");
        builder.HasKey(cr => cr.Id);
        builder.Property(cr => cr.Name).HasMaxLength(500).IsRequired();
        builder.Property(cr => cr.Email).HasMaxLength(500).IsRequired();
        builder.HasIndex(a => new { a.Id });
        builder.HasIndex(a => a.Email).IsUnique();
    }
}
