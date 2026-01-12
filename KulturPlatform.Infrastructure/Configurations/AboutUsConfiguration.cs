using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class AboutUsConfiguration : IEntityTypeConfiguration<AboutUs>
    {
        public void Configure(EntityTypeBuilder<AboutUs> builder)
        {
            // Table
            builder.ToTable("AboutUs");
            builder.HasKey(a => a.Id);

            // Quote
            builder.OwnsOne(a => a.QuoteTr, q =>
            {
                q.Property(p => p.Value)
                    .HasColumnName("QuoteTr")
                    .HasMaxLength(2000)
                    .IsRequired(false);
            });

            builder.OwnsOne(a => a.QuoteDe, q =>
            {
                q.Property(p => p.Value)
                    .HasColumnName("QuoteDe")
                    .HasMaxLength(2000)
                    .IsRequired(false);
            });

            builder.Property(a => a.QuoteAuthor)
                .HasMaxLength(255)
                .IsRequired(false);

            // WhoWeAre
            builder.OwnsOne(a => a.WhoWeAreTr, ow =>
            {
                ow.Property(p => p.Value)
                    .HasColumnName("WhoWeAreTr")
                    .HasMaxLength(4000)
                    .IsRequired(false);
            });
            builder.OwnsOne(a => a.WhoWeAreDe, ow =>
            {
                ow.Property(p => p.Value)
                    .HasColumnName("WhoWeAreDe")
                    .HasMaxLength(4000)
                    .IsRequired(false);
            });

            // Goals
            builder.OwnsOne(a => a.GoalsTr, g =>
            {
                g.Property(p => p.Value)
                    .HasColumnName("GoalsTr")
                    .HasMaxLength(4000)
                    .IsRequired(false);
            });
            builder.OwnsOne(a => a.GoalsDe, g =>
            {
                g.Property(p => p.Value)
                    .HasColumnName("GoalsDe")
                    .HasMaxLength(4000)
                    .IsRequired(false);
            });

            // Vision
            builder.OwnsOne(a => a.VisionTr, v =>
            {
                v.Property(p => p.Value)
                    .HasColumnName("VisionTr")
                    .HasMaxLength(4000)
                    .IsRequired(false);
            });
            builder.OwnsOne(a => a.VisionDe, v =>
            {
                v.Property(p => p.Value)
                    .HasColumnName("VisionDe")
                    .HasMaxLength(4000)
                    .IsRequired(false);
            });

            // Mission
            builder.OwnsOne(a => a.MissionTr, m =>
            {
                m.Property(p => p.Value)
                    .HasColumnName("MissionTr")
                    .HasMaxLength(4000)
                    .IsRequired(false);
            });
            builder.OwnsOne(a => a.MissionDe, m =>
            {
                m.Property(p => p.Value)
                    .HasColumnName("MissionDe")
                    .HasMaxLength(4000)
                    .IsRequired(false);
            });

            // Child collections - use NoAction to avoid multiple cascade paths
            builder.HasMany(a => a.CoreValues)
                .WithOne()
                .HasForeignKey("AboutUsId_CoreValues")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.FocusAreas)
                .WithOne()
                .HasForeignKey("AboutUsId_FocusAreas")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.ActivityAreas)
                .WithOne()
                .HasForeignKey("AboutUsId_ActivityAreas")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.TeamMembers)
                .WithOne()
                .HasForeignKey("AboutUsId_TeamMembers")
                .OnDelete(DeleteBehavior.NoAction);

            // Optimistic concurrency token
            builder.Property<byte[]>("RowVersion")
                .IsRowVersion()
                .HasColumnName("RowVersion");
        }
    }
}
