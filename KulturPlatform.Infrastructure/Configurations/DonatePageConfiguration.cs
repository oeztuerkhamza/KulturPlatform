using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class DonatePageConfiguration : IEntityTypeConfiguration<DonatePage>
    {
        public void Configure(EntityTypeBuilder<DonatePage> builder)
        {
            builder.ToTable("DonatePages");
            builder.HasKey(x => x.Id);

            // Hero Section
            builder.OwnsOne(x => x.HeroTitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("HeroTitleTurkish")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.HeroTitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("HeroTitleGerman")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.HeroSubtitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("HeroSubtitleTurkish")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.HeroSubtitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("HeroSubtitleGerman")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.HeroImageUrl, vo =>
            {
                vo.Property(u => u.Value)
                  .HasColumnName("HeroImageUrl")
                  .HasMaxLength(500)
                  .IsRequired(false);
            });

            // Feature Highlights
            builder.OwnsOne(x => x.Feature1TitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("Feature1TitleTurkish")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.Feature1TitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("Feature1TitleGerman")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.Feature2TitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("Feature2TitleTurkish")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.Feature2TitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("Feature2TitleGerman")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.Feature3TitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("Feature3TitleTurkish")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.Feature3TitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("Feature3TitleGerman")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            // Why Donate Section
            builder.OwnsOne(x => x.WhyDonateTitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("WhyDonateTitleTurkish")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.WhyDonateTitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("WhyDonateTitleGerman")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.WhyDonateDescriptionTurkish, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("WhyDonateDescriptionTurkish")
                  .HasMaxLength(2000)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.WhyDonateDescriptionGerman, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("WhyDonateDescriptionGerman")
                  .HasMaxLength(2000)
                  .IsRequired();
            });

            // Where Section
            builder.OwnsOne(x => x.WhereTitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("WhereTitleTurkish")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.WhereTitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("WhereTitleGerman")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.WhereDescriptionTurkish, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("WhereDescriptionTurkish")
                  .HasMaxLength(2000)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.WhereDescriptionGerman, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("WhereDescriptionGerman")
                  .HasMaxLength(2000)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.TaxInfoTurkish, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("TaxInfoTurkish")
                  .HasMaxLength(1000)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.TaxInfoGerman, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("TaxInfoGerman")
                  .HasMaxLength(1000)
                  .IsRequired();
            });

            // Bank Account Details
            builder.Property(x => x.AccountHolder)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Iban)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.BicSwift)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.BankName)
                   .HasMaxLength(200)
                   .IsRequired();

            // PayPal Details
            builder.OwnsOne(x => x.PayPalUrl, vo =>
            {
                vo.Property(u => u.Value)
                  .HasColumnName("PayPalUrl")
                  .HasMaxLength(500)
                  .IsRequired();
            });

            builder.Property(x => x.PayPalHandle)
                   .HasMaxLength(100)
                   .IsRequired();

            // Legacy Content
            builder.Property(x => x.ContentTurkish)
                   .HasMaxLength(4000)
                   .IsRequired();

            builder.Property(x => x.ContentGerman)
                   .HasMaxLength(4000)
                   .IsRequired();

            // Audit fields
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
        }
    }
}
