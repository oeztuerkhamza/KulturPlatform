using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class NewsletterSubscriberConfiguration : IEntityTypeConfiguration<NewsletterSubscriber>
    {
        public void Configure(EntityTypeBuilder<NewsletterSubscriber> builder)
        {
            builder.ToTable("NewsletterSubscribers");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .ValueGeneratedNever();

            builder.OwnsOne(s => s.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(255)
                    .IsRequired();

                email.HasIndex(e => e.Value)
                    .IsUnique();
            });

            builder.Property(s => s.FullName)
                .HasMaxLength(200);

            builder.Property(s => s.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(s => s.IsVerified)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(s => s.SubscribedAt)
                .IsRequired();

            builder.Property(s => s.VerifiedAt);

            builder.Property(s => s.UnsubscribedAt);

            builder.Property(s => s.UnsubscribeToken)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(s => s.UnsubscribeToken)
                .IsUnique();

            builder.Property(s => s.VerificationToken)
                .HasMaxLength(50);

            builder.HasIndex(s => s.VerificationToken)
                .IsUnique()
                .HasFilter("[VerificationToken] IS NOT NULL");

            builder.Property(s => s.Source)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
