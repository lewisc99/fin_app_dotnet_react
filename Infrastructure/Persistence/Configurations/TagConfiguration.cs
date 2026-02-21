using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();

            // Enforce uniqueness so we don't have duplicate "VIP" tags
            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.Property(t => t.Description)
                .HasMaxLength(200);
        }
    }
}
