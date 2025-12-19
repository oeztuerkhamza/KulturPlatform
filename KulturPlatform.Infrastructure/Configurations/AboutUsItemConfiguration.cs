using KulturPlatform.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class AboutUsItemConfiguration : IEntityTypeConfiguration<AboutUsItem>
    {
        public void Configure(EntityTypeBuilder<AboutUsItem> builder)
        {
            builder.ToTable("AboutUsItems");
            builder.HasKey(x => x.Id);

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
                d.Property(p => p.Value).HasColumnName("DescriptionTr").HasMaxLength(4000).IsRequired(false);
            });

            builder.OwnsOne(x => x.DescriptionDe, d =>
            {
                d.Property(p => p.Value).HasColumnName("DescriptionDe").HasMaxLength(4000).IsRequired(false);
            });

            builder.Property(x => x.Order).HasColumnName("Order");

            // Add optional FK columns for the different relationships
            builder.Property<Guid?>("AboutUsId_CoreValues");
            builder.Property<Guid?>("AboutUsId_FocusAreas");
            builder.Property<Guid?>("AboutUsId_ActivityAreas");
            builder.Property<Guid?>("AboutUsId_TeamMembers");
        }
    }
}
