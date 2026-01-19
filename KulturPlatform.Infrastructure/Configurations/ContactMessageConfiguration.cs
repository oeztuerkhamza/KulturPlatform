using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class ContactMessageConfiguration : IEntityTypeConfiguration<ContactMessage>
    {
        public void Configure(EntityTypeBuilder<ContactMessage> builder)
        {
            builder.ToTable("ContactMessages");
            builder.HasKey(c => c.Id);

            // ----- Anrede (Value Object) - Optional -----
            builder.OwnsOne(c => c.Anrede, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Anrede")
                  .HasMaxLength(50)
                  .IsRequired(false);
            });

            // ----- SenderName (Value Object) -----
            builder.OwnsOne(c => c.SenderName, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("SenderName")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            // ----- Email (Value Object) -----
            builder.OwnsOne(c => c.Email, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Email")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            // ----- Phone (Value Object) - Optional -----
            builder.OwnsOne(c => c.Phone, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Phone")
                  .HasMaxLength(50)
                  .IsRequired(false);
            });

            // ----- Subject (Value Object) -----
            builder.OwnsOne(c => c.Subject, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Subject")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            // ----- Message (Value Object) -----
            builder.OwnsOne(c => c.Message, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Message")
                  .HasMaxLength(5000)
                  .IsRequired();
            });

            builder.Property(c => c.SubmittedAt)
                   .IsRequired();

            builder.Property(c => c.IsRead)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(c => c.ReadAt)
                   .IsRequired(false);

            // Index for faster queries
            builder.HasIndex(c => c.SubmittedAt);
            builder.HasIndex(c => c.IsRead);
        }
    }
}
