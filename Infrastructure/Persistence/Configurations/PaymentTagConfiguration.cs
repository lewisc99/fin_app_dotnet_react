using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PaymentTagConfiguration : IEntityTypeConfiguration<PaymentTag>
    {
        public void Configure(EntityTypeBuilder<PaymentTag> builder)
        {
            // 1. Define Composite Primary Key
            builder.HasKey(pt => new { pt.PaymentId, pt.TagId });

            // 2. Configure Relationships
            builder.HasOne(pt => pt.Payment)
                .WithMany(p => p.Tags)       // Payment has many PaymentTags
                .HasForeignKey(pt => pt.PaymentId)
                .OnDelete(DeleteBehavior.Cascade); // Deleting a payment deletes its tags

            builder.HasOne(pt => pt.Tag)
                .WithMany(t => t.Payments)   // Tag has many PaymentTags
                .HasForeignKey(pt => pt.TagId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a Tag if it's in use

            // 3. Strongly Typed ID Conversion for the Foreign Key
            builder.Property(pt => pt.PaymentId)
                .HasConversion(id => id.Value, value => new PaymentId(value));

            builder.ToTable("PaymentTags");
        }
    }
}
