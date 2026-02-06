using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class NewsletterCampaignConfiguration : IEntityTypeConfiguration<NewsletterCampaign>
    {
        public void Configure(EntityTypeBuilder<NewsletterCampaign> builder)
        {
            builder.ToTable("NewsletterCampaigns");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedNever();

            builder.OwnsOne(c => c.Subject, subject =>
            {
                subject.Property(s => s.Value)
                    .HasColumnName("Subject")
                    .HasMaxLength(500)
                    .IsRequired();
            });

            builder.OwnsOne(c => c.ContentTr, content =>
            {
                content.Property(d => d.Value)
                    .HasColumnName("ContentTr")
                    .HasColumnType("nvarchar(max)")
                    .IsRequired();
            });

            builder.OwnsOne(c => c.ContentDe, content =>
            {
                content.Property(d => d.Value)
                    .HasColumnName("ContentDe")
                    .HasColumnType("nvarchar(max)")
                    .IsRequired();
            });

            builder.Property(c => c.HeaderImageUrl)
                .HasMaxLength(500);

            builder.Property(c => c.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.ScheduledAt);

            builder.Property(c => c.SentAt);

            builder.Property(c => c.TotalRecipients)
                .HasDefaultValue(0);

            builder.Property(c => c.SuccessfulSends)
                .HasDefaultValue(0);

            builder.Property(c => c.FailedSends)
                .HasDefaultValue(0);

            builder.Property(c => c.CreatedBy)
                .IsRequired();
        }
    }
}
