using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admins");

            builder.HasKey(a => a.Id);

            builder.OwnsOne(a => a.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(100);

                email.HasIndex(e => e.Value)
                    .IsUnique();
            });

            builder.OwnsOne(a => a.Password, password =>
            {
                password.Property(p => p.Value)
                    .HasColumnName("PasswordHash")
                    .IsRequired()
                    .HasMaxLength(255);
            });

            builder.OwnsOne(a => a.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(100);
            });

            builder.Property(a => a.Role)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("User");

            builder.Property(a => a.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(a => a.LastLoginAt)
                .IsRequired(false);

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.Property(a => a.UpdatedAt)
                .IsRequired(false);
        }
    }
}
