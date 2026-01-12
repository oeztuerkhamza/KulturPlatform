using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Commons.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class HeroSectionConfiguration : IEntityTypeConfiguration<HeroSection>
    {
        public void Configure(EntityTypeBuilder<HeroSection> builder)
        {
            builder.ToTable("HeroSections");
            builder.HasKey(h => h.Id);

            var titleConverter = new ValueConverter<Title, string>(v => v.Value, v => new Title(v));
            var descriptionConverter = new ValueConverter<Description, string>(v => v.Value, v => new Description(v));
            var urlConverter = new ValueConverter<Url, string>(v => v.Value, v => Url.Create(v));

            builder.Property(h => h.TitleTr)
                .HasConversion(titleConverter)
                .HasColumnName("TitleTr")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(h => h.TitleDe)
                .HasConversion(titleConverter)
                .HasColumnName("TitleDe")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(h => h.SubtitleTr)
                .HasConversion(titleConverter)
                .HasColumnName("SubtitleTr")
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(h => h.SubtitleDe)
                .HasConversion(titleConverter)
                .HasColumnName("SubtitleDe")
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(h => h.DescriptionTr)
                .HasConversion(descriptionConverter)
                .HasColumnName("DescriptionTr")
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(h => h.DescriptionDe)
                .HasConversion(descriptionConverter)
                .HasColumnName("DescriptionDe")
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(h => h.BackgroundImageUrl)
                .HasConversion(urlConverter)
                .HasColumnName("BackgroundImageUrl")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(h => h.PrimaryButtonTextTr)
                .HasConversion(titleConverter)
                .HasColumnName("PrimaryButtonTextTr")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(h => h.PrimaryButtonTextDe)
                .HasConversion(titleConverter)
                .HasColumnName("PrimaryButtonTextDe")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(h => h.SecondaryButtonTextTr)
                .HasConversion(titleConverter)
                .HasColumnName("SecondaryButtonTextTr")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(h => h.SecondaryButtonTextDe)
                .HasConversion(titleConverter)
                .HasColumnName("SecondaryButtonTextDe")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(h => h.CreatedAt).IsRequired();
            builder.Property(h => h.UpdatedAt);
        }
    }
}
