using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public sealed class TeaEventConfiguration
    : IEntityTypeConfiguration<TeaEvent>
    {
        public void Configure(EntityTypeBuilder<TeaEvent> builder)
        {
            builder.ToTable("TeaEvents");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.Date)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Time)
                .IsRequired();

            // 🔹 Title (VO)
            builder.OwnsOne(x => x.TitleTurkish, t =>
            {
                t.Property(p => p.Value)
                    .HasColumnName("TitleTr")
                    .HasMaxLength(200)
                    .IsRequired();
            });

            builder.OwnsOne(x => x.TitleGerman, t =>
            {
                t.Property(p => p.Value)
                    .HasColumnName("TitleDe")
                    .HasMaxLength(200)
                    .IsRequired();
            });

            // 🔹 Location (VO)
            builder.OwnsOne(x => x.Location, l =>
            {
                l.Property(p => p.Value)
                    .HasColumnName("Location")
                    .HasMaxLength(200)
                    .IsRequired();
            });

            // ✅ ImageUrl (URL option)
            builder.OwnsOne(x => x.ImageUrl, url =>
            {
                url.Property(u => u.Value)
                   .HasColumnName("ImageUrl")
                   .HasMaxLength(500);
            });

            // ✅ ImageData (Database option)
            builder.OwnsOne(x => x.ImageData, data =>
            {
                data.Property(d => d.Base64Data)
                    .HasColumnName("Image_Base64")
                    .HasColumnType("TEXT");

                data.Property(d => d.MimeType)
                    .HasColumnName("Image_MimeType")
                    .HasMaxLength(50);

                data.Property(d => d.FileName)
                    .HasColumnName("Image_FileName")
                    .HasMaxLength(255);

                data.Property(d => d.FileSizeBytes)
                    .HasColumnName("Image_FileSize");
            });

            // 🔹 Content (Owned VO)
            builder.OwnsOne(x => x.Content, c =>
            {
                c.Property(p => p.IntroTr)
                    .HasColumnName("IntroTr")
                    .IsRequired();

                c.Property(p => p.IntroDe)
                    .HasColumnName("IntroDe")
                    .IsRequired();

                c.Property(p => p.HeritageTextTr)
                    .HasColumnName("HeritageTextTr")
                    .IsRequired();

                c.Property(p => p.HeritageTextDe)
                    .HasColumnName("HeritageTextDe")
                    .IsRequired();

                c.Property(p => p.ParticipationTextTr)
                    .HasColumnName("ParticipationTextTr")
                    .IsRequired();

                c.Property(p => p.ParticipationTextDe)
                    .HasColumnName("ParticipationTextDe")
                    .IsRequired();

                c.Property(p => p.ContactEmail)
                    .HasColumnName("ContactEmail")
                    .HasMaxLength(200)
                    .IsRequired();
            });
        }
    }
}
