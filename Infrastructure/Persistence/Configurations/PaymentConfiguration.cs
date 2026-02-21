using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // 1. Primary Key
            builder.HasKey(p => p.Id);

            // 2. Strongly Typed ID Conversion (PaymentId <-> Guid)
            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => new PaymentId(value));

            // 3. Complex Value Object (Money)
            // Splits "Amount" property into "Amount_Value" and "Amount_Currency" columns
            builder.OwnsOne(p => p.Amount, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("Amount")
                    .HasColumnType("decimal(18,4)") // Precision is key for Finance
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("Currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            // 4. Enums as Strings (Better readability in SQL than int 0, 1, 2)
            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(p => p.Method)
                .HasConversion<string>()
                .HasMaxLength(20);

            // 5. Audit Fields
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.UpdatedAt);

            // 6. Navigation to Tags (Many-to-Many)
            // We tell EF to access the private collection field "_tags"
            builder.Metadata
                .FindNavigation(nameof(Payment.Tags))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
