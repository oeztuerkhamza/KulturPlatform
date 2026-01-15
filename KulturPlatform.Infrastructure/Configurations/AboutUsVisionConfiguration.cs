using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations;

public class AboutUsVisionConfiguration : IEntityTypeConfiguration<AboutUsVision>
{
    public void Configure(EntityTypeBuilder<AboutUsVision> builder)
    {
        builder.ToTable("AboutUsVision");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.VisionTr, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("VisionTr")
                .HasMaxLength(5000)
                .IsRequired();
        });

        builder.OwnsOne(x => x.VisionDe, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("VisionDe")
                .HasMaxLength(5000)
                .IsRequired();
        });

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}
