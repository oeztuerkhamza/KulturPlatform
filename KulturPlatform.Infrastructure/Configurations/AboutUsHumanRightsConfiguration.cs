using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations;

public class AboutUsHumanRightsConfiguration : IEntityTypeConfiguration<AboutUsHumanRights>
{
    public void Configure(EntityTypeBuilder<AboutUsHumanRights> builder)
    {
        builder.ToTable("AboutUsHumanRights");

        builder.HasKey(x => x.Id);

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
                .HasMaxLength(5000)
                .IsRequired();
        });

        builder.OwnsOne(x => x.DescriptionDe, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("DescriptionDe")
                .HasMaxLength(5000)
                .IsRequired();
        });

        builder.Property(x => x.TenkilMuseumUrl)
            .HasMaxLength(500);

        builder.Property(x => x.InstagramUrl)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}
