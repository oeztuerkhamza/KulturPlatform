using KulturPlatform.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations;

public class FocusAreaConfiguration : IEntityTypeConfiguration<FocusArea>
{
    public void Configure(EntityTypeBuilder<FocusArea> builder)
    {
        builder.ToTable("FocusAreas");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.TitleTr, vo =>
        {
            vo.Property(t => t.Value)
              .HasColumnName("TitleTr")
              .HasMaxLength(200)
              .IsRequired();
        });

        builder.OwnsOne(x => x.TitleDe, vo =>
        {
            vo.Property(t => t.Value)
              .HasColumnName("TitleDe")
              .HasMaxLength(200)
              .IsRequired();
        });

        builder.OwnsOne(x => x.DescriptionTr, vo =>
        {
            vo.Property(d => d.Value)
              .HasColumnName("DescriptionTr")
              .HasMaxLength(2000)
              .IsRequired();
        });

        builder.OwnsOne(x => x.DescriptionDe, vo =>
        {
            vo.Property(d => d.Value)
              .HasColumnName("DescriptionDe")
              .HasMaxLength(2000)
              .IsRequired();
        });

        // ? IconUrl (URL option)
        builder.OwnsOne(x => x.IconUrl, url =>
        {
            url.Property(u => u.Value)
               .HasColumnName("IconUrl")
               .HasMaxLength(500);
        });

        // ? IconData (Database option)
        builder.OwnsOne(x => x.IconData, data =>
        {
            data.Property(d => d.Base64Data)
                .HasColumnName("Icon_Base64")
                .HasColumnType("TEXT");

            data.Property(d => d.MimeType)
                .HasColumnName("Icon_MimeType")
                .HasMaxLength(50);

            data.Property(d => d.FileName)
                .HasColumnName("Icon_FileName")
                .HasMaxLength(255);

            data.Property(d => d.FileSizeBytes)
                .HasColumnName("Icon_FileSize");
        });

        builder.Property(x => x.Order).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt);
    }
}
