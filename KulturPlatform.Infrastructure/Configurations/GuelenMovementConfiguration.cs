using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class GuelenMovementConfiguration : IEntityTypeConfiguration<GuelenMovement>
    {
        public void Configure(EntityTypeBuilder<GuelenMovement> builder)
        {
            builder.ToTable("GuelenMovements");
            builder.HasKey(x => x.Id);

            // Title Turkish (Value Object)
            builder.OwnsOne(x => x.TitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("TitleTurkish")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            // Title German (Value Object)
            builder.OwnsOne(x => x.TitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("TitleGerman")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            // Content Turkish
            builder.Property(x => x.ContentTurkish)
                   .HasMaxLength(4000)
                   .IsRequired();

            // Content German
            builder.Property(x => x.ContentGerman)
                   .HasMaxLength(4000)
                   .IsRequired();

            // Image URL (Value Object)
            builder.OwnsOne(x => x.ImageUrl, vo =>
            {
                vo.Property(u => u.Value)
                  .HasColumnName("ImageUrl")
                  .HasMaxLength(500)
                  .IsRequired();
            });

            // Audit fields
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
        }
    }
}
