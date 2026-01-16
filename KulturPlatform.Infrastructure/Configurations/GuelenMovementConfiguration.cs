using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class GuelenMovementConfiguration : IEntityTypeConfiguration<GuelenMovement>
    {
        public void Configure(EntityTypeBuilder<GuelenMovement> builder)
        {
            builder.ToTable("GuelenMovements");
            builder.HasKey(x => x.Id);

            // Main Content
            builder.OwnsOne(x => x.TitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("TitleTurkish")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.TitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("TitleGerman")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.IntroductionTurkish, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("IntroductionTurkish")
                  .HasMaxLength(2000)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.IntroductionGerman, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("IntroductionGerman")
                  .HasMaxLength(2000)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.ImageUrl, vo =>
            {
                vo.Property(u => u.Value)
                  .HasColumnName("ImageUrl")
                  .HasMaxLength(500)
                  .IsRequired();
            });

            // Philosophy Section
            builder.OwnsOne(x => x.PhilosophyTitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("PhilosophyTitleTurkish")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.PhilosophyTitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("PhilosophyTitleGerman")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.PhilosophyContentTurkish, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("PhilosophyContentTurkish")
                  .HasMaxLength(5000)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.PhilosophyContentGerman, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("PhilosophyContentGerman")
                  .HasMaxLength(5000)
                  .IsRequired();
            });

            // Dialog Section
            builder.OwnsOne(x => x.DialogTitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("DialogTitleTurkish")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.DialogTitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("DialogTitleGerman")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.DialogContentTurkish, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("DialogContentTurkish")
                  .HasMaxLength(5000)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.DialogContentGerman, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("DialogContentGerman")
                  .HasMaxLength(5000)
                  .IsRequired();
            });

            // Network Section
            builder.OwnsOne(x => x.NetworkTitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("NetworkTitleTurkish")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.NetworkTitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("NetworkTitleGerman")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.NetworkContentTurkish, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("NetworkContentTurkish")
                  .HasMaxLength(5000)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.NetworkContentGerman, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("NetworkContentGerman")
                  .HasMaxLength(5000)
                  .IsRequired();
            });

            // Spiritual Roots Section
            builder.OwnsOne(x => x.SpiritualTitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("SpiritualTitleTurkish")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.SpiritualTitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("SpiritualTitleGerman")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.SpiritualContentTurkish, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("SpiritualContentTurkish")
                  .HasMaxLength(5000)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.SpiritualContentGerman, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("SpiritualContentGerman")
                  .HasMaxLength(5000)
                  .IsRequired();
            });

            // Vision Section
            builder.OwnsOne(x => x.VisionTitleTurkish, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("VisionTitleTurkish")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.VisionTitleGerman, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("VisionTitleGerman")
                  .HasMaxLength(300)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.VisionContentTurkish, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("VisionContentTurkish")
                  .HasMaxLength(5000)
                  .IsRequired();
            });

            builder.OwnsOne(x => x.VisionContentGerman, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("VisionContentGerman")
                  .HasMaxLength(5000)
                  .IsRequired();
            });

            // Audit fields
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
        }
    }
}
