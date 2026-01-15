using KulturPlatform.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.ToTable("TeamMembers");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(200)
                    .IsRequired();
            });

            builder.OwnsOne(x => x.TitleTr, title =>
            {
                title.Property(t => t.Value)
                    .HasColumnName("TitleTr")
                    .HasMaxLength(200)
                    .IsRequired();
            });

            builder.OwnsOne(x => x.TitleDe, title =>
            {
                title.Property(t => t.Value)
                    .HasColumnName("TitleDe")
                    .HasMaxLength(200)
                    .IsRequired();
            });

            builder.OwnsOne(x => x.DescriptionTr, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("DescriptionTr")
                    .HasMaxLength(2000)
                    .IsRequired(false);
            });

            builder.OwnsOne(x => x.DescriptionDe, desc =>
            {
                desc.Property(d => d.Value)
                    .HasColumnName("DescriptionDe")
                    .HasMaxLength(2000)
                    .IsRequired(false);
            });

            builder.Property(x => x.ImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.Order)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);
        }
    }
}
