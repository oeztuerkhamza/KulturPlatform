using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class ValueItemConfiguration : IEntityTypeConfiguration<ValueItem>
    {
        public void Configure(EntityTypeBuilder<ValueItem> builder)
        {
            builder.ToTable("ValueItems");

            builder.HasKey(x => x.Id);

            // ----- Title (ValueObject) -----
            builder.OwnsOne(x => x.TitleTr, titleTr =>
            {
                titleTr.Property(t => t.Value)
                       .HasColumnName("TitleTr")
                       .HasMaxLength(200)
                       .IsRequired();
            });

            builder.OwnsOne(x => x.TitleDe, titleDe =>
            {
                titleDe.Property(t => t.Value)
                       .HasColumnName("TitleDe")
                       .HasMaxLength(200)
                       .IsRequired();
            });
            // ----- Subtitle (ValueObject) -----
            builder.OwnsOne(x => x.SubtitleTr, subtitleTr =>
            {
                subtitleTr.Property(s => s.Value)
                           .HasColumnName("SubtitleTr")
                           .HasMaxLength(200)
                           .IsRequired();
            });
            builder.OwnsOne(x => x.SubtitleDe, subtitleDe =>
            {
                subtitleDe.Property(s => s.Value)
                           .HasColumnName("SubtitleDe")
                           .HasMaxLength(200)
                           .IsRequired();
            });


            // ----- Description (ValueObject) -----
            builder.OwnsOne(x => x.DescriptionTr, descriptionTr =>
            {
                descriptionTr.Property(d => d.Value)
                             .HasColumnName("DescriptionTr")
                             .HasMaxLength(1000)
                             .IsRequired();
            });

            builder.OwnsOne(x => x.DescriptionDe, descriptionDe =>
            {
                descriptionDe.Property(d => d.Value)
                             .HasColumnName("DescriptionDe")
                             .HasMaxLength(1000)
                             .IsRequired();
            });

            // ----- DisplayOrder (ValueObject) -----
            builder.OwnsOne(x => x.DisplayOrder, displayOrder =>
            {
                displayOrder.Property(d => d.Value)
                            .HasColumnName("DisplayOrder")
                            .IsRequired();
            });

            // ----- IsActive -----
            builder.Property(x => x.IsActive)
                   .IsRequired();

            // ----- Auditable -----
            builder.Property(x => x.CreatedAt)
                   .IsRequired();
            builder.Property(x => x.UpdatedAt);

            // ----- Sections -----
            builder.OwnsMany(x => x.Sections, section =>
            {
                section.ToTable("ValueItemSections");

                section.WithOwner().HasForeignKey("ValueItemId");
                section.HasKey("Id"); // Section bir entity, PK gerekli

                section.OwnsOne(s => s.HeadingTr, hTr =>
                {
                    hTr.Property(h => h.Value)
                        .HasColumnName("HeadingTr")
                        .HasMaxLength(200)
                        .IsRequired();
                });
                section.OwnsOne(s => s.HeadingDe, hDe =>
                {
                    hDe.Property(h => h.Value)
                        .HasColumnName("HeadingDe")
                        .HasMaxLength(200)
                        .IsRequired();
                });

                section.OwnsOne(s => s.BodyTr, bTr =>
                {
                    bTr.Property(b => b.Value)
                        .HasColumnName("BodyTr")
                        .HasMaxLength(1000)
                        .IsRequired();
                });
                section.OwnsOne(s => s.BodyDe, bDe =>
                {
                    bDe.Property(b => b.Value)
                        .HasColumnName("BodyDe")
                        .HasMaxLength(1000)
                        .IsRequired();
                });

                // SectionItem VO
                section.OwnsMany(s => s.Items, item =>
                {
                    item.ToTable("SectionItems");

                    item.WithOwner().HasForeignKey("SectionId");

                    // EF Core otomatik shadow key oluşturacak
                    // HasKey tanımlama
                    item.OwnsOne(i => i.TitleTr, tTr =>
                    {
                        tTr.Property(t => t.Value)
                            .HasColumnName("TitleTr")
                            .HasMaxLength(200)
                            .IsRequired();
                    });
                    item.OwnsOne(i => i.TitleDe, tDe =>
                    {
                        tDe.Property(t => t.Value)
                            .HasColumnName("TitleDe")
                            .HasMaxLength(200)
                            .IsRequired();
                    });


                    item.Property(i => i.Icon)
                        .HasMaxLength(200);
                });
            });

        }
    }
}
