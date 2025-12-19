using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class PageContentConfiguration : IEntityTypeConfiguration<PageContent>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PageContent> builder)
        {
            builder.ToTable("PageContents");
            builder.HasKey(pc => pc.Id);

            builder.OwnsOne(pc => pc.PageName, pn =>
            {
                pn.Property(p => p.Value).HasColumnName("PageName").HasMaxLength(100).IsRequired();
            });

            builder.OwnsOne(pc => pc.SectionKey, sk =>
            {
                sk.Property(s => s.Value).HasColumnName("SectionKey").HasMaxLength(100).IsRequired();
            });

            builder.OwnsOne(pc => pc.ContentTr, lc =>
            {
                lc.Property(c => c.Value).HasColumnName("ContentTr").HasMaxLength(4000).IsRequired();
            });

            builder.OwnsOne(pc => pc.ContentDe, lc =>
            {
                lc.Property(c => c.Value).HasColumnName("ContentDe").HasMaxLength(4000).IsRequired();
            });

            builder.Property(pc => pc.IsActive).IsRequired();
            builder.Property(pc => pc.CreatedAt).IsRequired();
            builder.Property(pc => pc.UpdatedAt);
        }
    }
}
