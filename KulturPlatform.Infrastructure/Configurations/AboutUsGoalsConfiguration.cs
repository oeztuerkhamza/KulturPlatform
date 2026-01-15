using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations;

public class AboutUsGoalsConfiguration : IEntityTypeConfiguration<AboutUsGoals>
{
    public void Configure(EntityTypeBuilder<AboutUsGoals> builder)
    {
        builder.ToTable("AboutUsGoals");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.GoalsTr, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("GoalsTr")
                .HasMaxLength(5000)
                .IsRequired();
        });

        builder.OwnsOne(x => x.GoalsDe, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("GoalsDe")
                .HasMaxLength(5000)
                .IsRequired();
        });

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}
