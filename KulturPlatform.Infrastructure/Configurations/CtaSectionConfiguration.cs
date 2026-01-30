using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Commons.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class CtaSectionConfiguration : IEntityTypeConfiguration<CtaSection>
    {
        public void Configure(EntityTypeBuilder<CtaSection> builder)
        {
            builder.ToTable("CtaSections");
            builder.HasKey(c => c.Id);

            var titleConverter = new ValueConverter<Title, string>(v => v.Value, v => new Title(v));
            var descriptionConverter = new ValueConverter<Description, string>(v => v.Value, v => new Description(v));

            builder.Property(c => c.TitleTr)
                .HasConversion(titleConverter)
                .HasColumnName("TitleTr")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(c => c.TitleDe)
                .HasConversion(titleConverter)
                .HasColumnName("TitleDe")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(c => c.DescriptionTr)
                .HasConversion(descriptionConverter)
                .HasColumnName("DescriptionTr")
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(c => c.DescriptionDe)
                .HasConversion(descriptionConverter)
                .HasColumnName("DescriptionDe")
                .HasMaxLength(1000)
                .IsRequired();

            // ? BackgroundImageUrl (URL option)
            builder.OwnsOne(c => c.BackgroundImageUrl, url =>
            {
                url.Property(x => x.Value)
                   .HasColumnName("BackgroundImageUrl")
                   .HasMaxLength(500);
            });

            // ? BackgroundImageData (Database option)
            builder.OwnsOne(c => c.BackgroundImageData, data =>
            {
                data.Property(x => x.Base64Data)
                    .HasColumnName("BackgroundImage_Base64")
                    .HasColumnType("TEXT");

                data.Property(x => x.MimeType)
                    .HasColumnName("BackgroundImage_MimeType")
                    .HasMaxLength(50);

                data.Property(x => x.FileName)
                    .HasColumnName("BackgroundImage_FileName")
                    .HasMaxLength(255);

                data.Property(x => x.FileSizeBytes)
                    .HasColumnName("BackgroundImage_FileSize");
            });

            builder.Property(c => c.PrimaryButtonTr)
                .HasConversion(titleConverter)
                .HasColumnName("PrimaryButtonTr")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.PrimaryButtonDe)
                .HasConversion(titleConverter)
                .HasColumnName("PrimaryButtonDe")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.SecondaryButtonTr)
                .HasConversion(titleConverter)
                .HasColumnName("SecondaryButtonTr")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.SecondaryButtonDe)
                .HasConversion(titleConverter)
                .HasColumnName("SecondaryButtonDe")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.DonateButtonTr)
                .HasConversion(titleConverter)
                .HasColumnName("DonateButtonTr")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.DonateButtonDe)
                .HasConversion(titleConverter)
                .HasColumnName("DonateButtonDe")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.UpdatedAt);
        }
    }
}
