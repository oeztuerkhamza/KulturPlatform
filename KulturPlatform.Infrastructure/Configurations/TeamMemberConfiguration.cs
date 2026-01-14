using KulturPlatform.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.ToTable("AboutUsTeamMembers");
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Name, n =>
            {
                n.Property(p => p.Value).HasColumnName("Name").HasMaxLength(300).IsRequired(false);
            });

            builder.OwnsOne(x => x.TitleTr, t =>
            {
                t.Property(p => p.Value).HasColumnName("TitleTr").HasMaxLength(300).IsRequired(false);
            });

            builder.OwnsOne(x => x.TitleDe, t =>
            {
                t.Property(p => p.Value).HasColumnName("TitleDe").HasMaxLength(300).IsRequired(false);
            });

            builder.OwnsOne(x => x.DescriptionTr, d =>
            {
                d.Property(p => p.Value).HasColumnName("DescriptionTr").IsRequired(false);
            });

            builder.OwnsOne(x => x.DescriptionDe, d =>
            {
                d.Property(p => p.Value).HasColumnName("DescriptionDe").IsRequired(false);
            });

            builder.Property(x => x.ImageUrl).HasMaxLength(1000).IsRequired(false);
            builder.Property(x => x.Order).IsRequired();
        }
    }
}
