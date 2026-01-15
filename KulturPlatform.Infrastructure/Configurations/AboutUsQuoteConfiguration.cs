using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations;

public class AboutUsQuoteConfiguration : IEntityTypeConfiguration<AboutUsQuote>
{
    public void Configure(EntityTypeBuilder<AboutUsQuote> builder)
    {
        builder.ToTable("AboutUsQuotes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.QuoteAuthor)
            .IsRequired()
            .HasMaxLength(200);

        builder.OwnsOne(x => x.QuoteTr, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("QuoteTr")
                .HasMaxLength(2000)
                .IsRequired();
        });

        builder.OwnsOne(x => x.QuoteDe, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("QuoteDe")
                .HasMaxLength(2000)
                .IsRequired();
        });

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}
