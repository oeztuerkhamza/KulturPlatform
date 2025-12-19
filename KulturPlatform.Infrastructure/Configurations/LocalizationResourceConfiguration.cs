using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class LocalizationResourceConfiguration : IEntityTypeConfiguration<LocalizationResource>
    {
        public void Configure(EntityTypeBuilder<LocalizationResource> builder)
        {
            builder.ToTable("LocalizationResources");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Turkish)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x => x.German)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x => x.English)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x => x.Section)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .HasMaxLength(100);

            builder.Property(x => x.UpdatedBy)
                .HasMaxLength(100);

            // Indexes
            builder.HasIndex(x => x.Key)
                .IsUnique()
                .HasDatabaseName("IX_LocalizationResources_Key");

            builder.HasIndex(x => x.Section)
                .HasDatabaseName("IX_LocalizationResources_Section");

            builder.HasIndex(x => x.IsActive)
                .HasDatabaseName("IX_LocalizationResources_IsActive");
        }
    }
}
