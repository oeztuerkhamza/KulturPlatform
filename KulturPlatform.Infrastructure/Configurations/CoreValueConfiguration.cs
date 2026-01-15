using KulturPlatform.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations;

public class CoreValueConfiguration : IEntityTypeConfiguration<CoreValue>
{
    public void Configure(EntityTypeBuilder<CoreValue> builder)
    {
        builder.ToTable("CoreValues");

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
                .HasMaxLength(2000)
                .IsRequired();
        });

        builder.OwnsOne(x => x.DescriptionDe, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("DescriptionDe")
                .HasMaxLength(2000)
                .IsRequired();
        });

        builder.Property(x => x.Order)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}
