using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class VolunteerSubmissionConfiguration : IEntityTypeConfiguration<VolunteerSubmission>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VolunteerSubmission> builder)
        {
            builder.ToTable("VolunteerSubmissions");
            builder.HasKey(v => v.Id);

            // ----- FullName (Value Object) -----
            builder.OwnsOne(v => v.FullName, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("FullName")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            // ----- Email (Value Object) -----
            builder.OwnsOne(v => v.Email, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Email")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            // ----- PhoneNumber (Value Object) -----
            builder.OwnsOne(v => v.PhoneNumber, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("PhoneNumber")
                  .HasMaxLength(50)
                  .IsRequired();
            });

            // ----- Message (Value Object) -----
            builder.OwnsOne(v => v.Message, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Message")
                  .HasMaxLength(4000)
                  .IsRequired();
            });

            builder.Property(v => v.SubmittedAt)
                   .IsRequired();

        }
    }
}
