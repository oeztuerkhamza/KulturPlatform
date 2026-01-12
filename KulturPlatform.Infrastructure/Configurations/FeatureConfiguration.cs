using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Commons.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable("Features");
            builder.HasKey(f => f.Id);

            var titleConverter = new ValueConverter<Title, string>(v => v.Value, v => new Title(v));
            var descriptionConverter = new ValueConverter<Description, string>(v => v.Value, v => new Description(v));

            builder.Property(f => f.TitleTr)
                .HasConversion(titleConverter)
                .HasColumnName("TitleTr")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(f => f.TitleDe)
                .HasConversion(titleConverter)
                .HasColumnName("TitleDe")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(f => f.DescriptionTr)
                .HasConversion(descriptionConverter)
                .HasColumnName("DescriptionTr")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(f => f.DescriptionDe)
                .HasConversion(descriptionConverter)
                .HasColumnName("DescriptionDe")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(f => f.Color)
                .HasColumnName("Color")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(f => f.CreatedAt).IsRequired();
            builder.Property(f => f.UpdatedAt);
        }
    }
}
