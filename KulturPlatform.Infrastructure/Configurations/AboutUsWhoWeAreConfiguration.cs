using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations;

public class AboutUsWhoWeAreConfiguration : IEntityTypeConfiguration<AboutUsWhoWeAre>
{
    public void Configure(EntityTypeBuilder<AboutUsWhoWeAre> builder)
    {
        builder.ToTable("AboutUsWhoWeAre");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.WhoWeAreTr, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("WhoWeAreTr")
                .HasMaxLength(5000)
                .IsRequired();
        });

        builder.OwnsOne(x => x.WhoWeAreDe, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("WhoWeAreDe")
                .HasMaxLength(5000)
                .IsRequired();
        });

        builder.OwnsOne(x => x.BannerImageUrl, url =>
        {
            url.Property(u => u.Value)
               .HasColumnName("BannerImageUrl")
               .HasMaxLength(500);
        });

        builder.OwnsOne(x => x.BannerImageData, data =>
        {
            data.Property(d => d.Base64Data)
                .HasColumnName("BannerImage_Base64")
                .HasColumnType("TEXT");

            data.Property(d => d.MimeType)
                .HasColumnName("BannerImage_MimeType")
                .HasMaxLength(50);

            data.Property(d => d.FileName)
                .HasColumnName("BannerImage_FileName")
                .HasMaxLength(255);

            data.Property(d => d.FileSizeBytes)
                .HasColumnName("BannerImage_FileSize");
        });

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}
