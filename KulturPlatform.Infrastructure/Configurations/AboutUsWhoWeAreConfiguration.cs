using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations;

public class AboutUsWhoWeAreConfiguration : IEntityTypeConfiguration<AboutUsWhoWeAre>
{
    public void Configure(EntityTypeBuilder<AboutUsWhoWeAre> builder)
    {
        builder.ToTable("AboutUsWhoWeAre");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.WhoWeAreTr, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("WhoWeAreTr")
                .HasMaxLength(5000)
                .IsRequired();
        });

        builder.OwnsOne(x => x.WhoWeAreDe, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("WhoWeAreDe")
                .HasMaxLength(5000)
                .IsRequired();
        });

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}
