using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.ToTable("Partners");
            builder.HasKey(p => p.Id);
            
            // ----- Name (Value Object) -----
            builder.OwnsOne(p => p.Name, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Name")
                  .HasMaxLength(200)
                  .IsRequired();
            });
            
            // ----- Description (Value Object) -----
            builder.OwnsOne(p => p.DescriptionTr, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("DescriptionTr")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            builder.OwnsOne(p => p.DescriptionDe, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("DescriptionDe")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            // ----- DisplayOrder (Value Object) -----
            builder.OwnsOne(p => p.DisplayOrder, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("DisplayOrder")
                  .IsRequired();
            });
            
            // ----- IsActive (Primitive) -----
            builder.Property(p => p.IsActive)
                   .HasColumnName("IsActive")
                   .IsRequired();
            
            // ----- WebsiteUrl (Value Object) -----
            builder.OwnsOne(p => p.WebsiteUrl, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("WebsiteUrl")
                  .HasMaxLength(500);
            });
            
            // ✅ LogoUrl (URL option)
            builder.OwnsOne(p => p.LogoUrl, url =>
            {
                url.Property(x => x.Value)
                   .HasColumnName("LogoUrl")
                   .HasMaxLength(500);
            });

            // ✅ LogoData (Database option)
            builder.OwnsOne(p => p.LogoData, data =>
            {
                data.Property(x => x.Base64Data)
                    .HasColumnName("Logo_Base64")
                    .HasColumnType("TEXT");

                data.Property(x => x.MimeType)
                    .HasColumnName("Logo_MimeType")
                    .HasMaxLength(50);

                data.Property(x => x.FileName)
                    .HasColumnName("Logo_FileName")
                    .HasMaxLength(255);

                data.Property(x => x.FileSizeBytes)
                    .HasColumnName("Logo_FileSize");
            });
            
            builder.Property(t => t.CreatedAt)
                .IsRequired();

            builder.Property(t => t.UpdatedAt);
        }
    }
}
