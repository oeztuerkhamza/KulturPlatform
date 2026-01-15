using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations;

public class AboutUsMissionConfiguration : IEntityTypeConfiguration<AboutUsMission>
{
    public void Configure(EntityTypeBuilder<AboutUsMission> builder)
    {
        builder.ToTable("AboutUsMission");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.MissionTr, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("MissionTr")
                .HasMaxLength(5000)
                .IsRequired();
        });

        builder.OwnsOne(x => x.MissionDe, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("MissionDe")
                .HasMaxLength(5000)
                .IsRequired();
        });

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}
