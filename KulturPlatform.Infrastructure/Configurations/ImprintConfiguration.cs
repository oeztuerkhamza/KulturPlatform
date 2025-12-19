using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class ImprintConfiguration : IEntityTypeConfiguration<Imprint>
    {
        public void Configure(EntityTypeBuilder<Imprint> builder)
        {
            builder.ToTable("Imprints");
            builder.HasKey(i => i.Id);

            // Organization Info
            var titleConverter = new ValueConverter<Title, string>(
                v => v.Value,      // DB'ye yazarken Title.Value kullan
                v => Title.Create(v)  // DB'den okurken Title nesnesi oluştur
            );
            builder.Property(i => i.OrganizationName)
                .HasConversion(titleConverter)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(i => i.OrganizationType)
                .IsRequired()
                .HasMaxLength(100);

            // Street (Address VO)
            builder.OwnsOne(i => i.Address, vo =>
            {
                vo.Property(a => a.Street).HasColumnName("Street").HasMaxLength(200);
                vo.Property(a => a.ZipCode).HasColumnName("PostalCode").HasMaxLength(20);
                vo.Property(a => a.City).HasColumnName("City").HasMaxLength(100);
                vo.Property(a => a.State).HasColumnName("State").HasMaxLength(100);
                vo.Property(a => a.Country).HasColumnName("Country").HasMaxLength(100);
            });

            // Email
            builder.OwnsOne(i => i.Email, vo =>
            {
                vo.Property(e => e.Value).HasColumnName("Email").IsRequired().HasMaxLength(200);
            });

            // Phone
            builder.OwnsOne(i => i.Phone, vo =>
            {
                vo.Property(p => p.Value).HasColumnName("Phone").HasMaxLength(50);
            });

            // President
            builder.OwnsOne(i => i.President, vo =>
            {
                vo.Property(n => n.Value).HasColumnName("PresidentFirstName").HasMaxLength(100);
                vo.Property(n => n.Value).HasColumnName("PresidentLastName").HasMaxLength(100);
            });

            // VicePresident
            builder.OwnsOne(i => i.President, vo =>
            {
                vo.Property(n => n.Value)
                    .HasColumnName("President")
                    .HasMaxLength(200)
                    .IsRequired();
            });

            builder.OwnsOne(i => i.VicePresident, vo =>
            {
                vo.Property(n => n.Value)
                    .HasColumnName("VicePresident")
                    .HasMaxLength(200);
            });


            // Legal Structure
            builder.Property(i => i.LegalStructureTurkish).HasMaxLength(500);
            builder.Property(i => i.LegalStructureGerman).HasMaxLength(500);

            // Purpose
            builder.Property(i => i.PurposeTurkish).HasMaxLength(1000);
            builder.Property(i => i.PurposeGerman).HasMaxLength(1000);

            // Tax Exemption
            builder.Property(i => i.TaxExemptionTurkish).HasMaxLength(500);
            builder.Property(i => i.TaxExemptionGerman).HasMaxLength(500);

            // Content Responsibility
            builder.Property(i => i.ContentResponsibilityTurkish).HasMaxLength(1000);
            builder.Property(i => i.ContentResponsibilityGerman).HasMaxLength(1000);

            // Links Responsibility
            builder.Property(i => i.LinksResponsibilityTurkish).HasMaxLength(1000);
            builder.Property(i => i.LinksResponsibilityGerman).HasMaxLength(1000);

            // Copyright
            builder.Property(i => i.CopyrightTurkish).HasMaxLength(500);
            builder.Property(i => i.CopyrightGerman).HasMaxLength(500);

            // Audit fields
            builder.Property(i => i.CreatedAt).IsRequired();
            builder.Property(i => i.UpdatedAt);
        }
    }
}
