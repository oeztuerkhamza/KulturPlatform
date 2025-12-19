using KulturPlatform.Domain.Commons.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activities");
            builder.HasKey(a => a.Id);

            // ----- Title (Value Object) -----
            builder.OwnsOne(a => a.TitleTr, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("TitleTr")
                  .HasMaxLength(200)
                  .IsRequired();
            });
            builder.OwnsOne(a => a.TitleDe, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("TitleDe")
                  .HasMaxLength(200)
                  .IsRequired();
            });

            // ----- Description (Value Object) -----
            builder.OwnsOne(a => a.DescriptionTr, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("DescriptionTr")
                  .HasMaxLength(1000)
                  .IsRequired();
            });
            builder.OwnsOne(a => a.DescriptionDe, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("DescriptionDe")
                  .HasMaxLength(1000)
                  .IsRequired();
            });

            // ----- DetailedContent -----
            builder.Property(a => a.DetailedContentTr)
                   .HasMaxLength(4000);
            builder.Property(a => a.DetailedContentDe)
                   .HasMaxLength(4000);

            // ----- Date (Value Object) -----
            builder.OwnsOne(a => a.DateTr, vo =>
            {
                vo.Property(x => x.DateTr).HasColumnName("DateTr_TextTr").HasMaxLength(100);
                vo.Property(x => x.DateDe).HasColumnName("DateTr_TextDe").HasMaxLength(100);
                vo.Property(x => x.DateISO).HasColumnName("DateTr_DateISO").IsRequired();
            });
            builder.OwnsOne(a => a.DateDe, vo =>
            {
                vo.Property(x => x.DateTr).HasColumnName("DateDe_TextTr").HasMaxLength(100);
                vo.Property(x => x.DateDe).HasColumnName("DateDe_TextDe").HasMaxLength(100);
                vo.Property(x => x.DateISO).HasColumnName("DateDe_DateISO").IsRequired();
            });

            // ----- Address (Value Object) -----
            builder.OwnsOne(a => a.Address, vo =>
            {
                vo.Property(x => x.Street).HasColumnName("Location_Address").HasMaxLength(200).IsRequired();
                vo.Property(x => x.City).HasColumnName("Location_City").HasMaxLength(100).IsRequired();
                vo.Property(x => x.State).HasColumnName("Location_State").HasMaxLength(100);
                vo.Property(x => x.Country).HasColumnName("Location_Country").HasMaxLength(100).IsRequired();
                vo.Property(x => x.ZipCode).HasColumnName("Location_ZipCode").HasMaxLength(20);
            });

            // ----- Category (Value Object) -----
            builder.OwnsOne(a => a.Category, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("Category")
                  .HasMaxLength(50)
                  .IsRequired();
            });

            // ----- ImageUrl (Value Object) -----
            builder.OwnsOne(a => a.ImageUrl, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("ImageUrl")
                  .HasMaxLength(500);
            });

            // ----- GalleryImages (Value Object with nested collection) -----
            builder.OwnsOne(a => a.GalleryImages, vo =>
            {
                vo.OwnsMany(g => g.Images, img =>
                {
                    img.ToTable("ActivityGalleryImages");
                    img.WithOwner().HasForeignKey("ActivityId");
                    img.Property(x => x.Value).HasColumnName("ImageUrl").HasMaxLength(500).IsRequired();
                });
            });

            // ----- VideoUrl (Value Object) -----
            builder.OwnsOne(a => a.VideoUrl, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("VideoUrl")
                  .HasMaxLength(500);
            });

            // ----- IsActive -----
            builder.Property(a => a.IsActive)
                   .IsRequired();

            // ----- CreatedAt -----
            builder.Property(a => a.CreatedAt)
                   .IsRequired();

            // ----- UpdatedAt -----
            builder.Property(a => a.UpdatedAt);
        }
    }
}
