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

            // Hero Title Turkish (Value Object)
            builder.OwnsOne(x => x.HeroTitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("HeroTitleTurkish")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            // Hero Title German (Value Object)
            builder.OwnsOne(x => x.HeroTitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("HeroTitleGerman")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            // Hero Subtitle Turkish (Value Object)
            builder.OwnsOne(x => x.HeroSubtitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("HeroSubtitleTurkish")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            // Hero Subtitle German (Value Object)
            builder.OwnsOne(x => x.HeroSubtitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("HeroSubtitleGerman")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            // Hero Image URL (Value Object)
            builder.OwnsOne(x => x.HeroImageUrl, vo =>
            {
                vo.Property(u => u.Value)
                  .HasColumnName("HeroImageUrl")
                  .HasMaxLength(500)
                  .IsRequired();
            });

            // Account Holder
            builder.Property(x => x.AccountHolder)
                   .HasMaxLength(200)
                   .IsRequired();

            // IBAN
            builder.Property(x => x.Iban)
                   .HasMaxLength(50)
                   .IsRequired();

            // Bank Name
            builder.Property(x => x.BankName)
                   .HasMaxLength(200)
                   .IsRequired();

            // Content Turkish
            builder.Property(x => x.ContentTurkish)
                   .HasMaxLength(4000)
                   .IsRequired();

            // Content German
            builder.Property(x => x.ContentGerman)
                   .HasMaxLength(4000)
                   .IsRequired();

            // Audit fields
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
        }
    }
}
