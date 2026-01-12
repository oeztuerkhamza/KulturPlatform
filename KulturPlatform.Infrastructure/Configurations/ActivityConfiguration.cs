using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Commons.ValueObjects;
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

            // ----- DetailedContent (optional Value Object) -----
            builder.OwnsOne(a => a.DetailedContentTr, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("DetailedContentTr")
                  .HasMaxLength(4000);
            });
            builder.OwnsOne(a => a.DetailedContentDe, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("DetailedContentDe")
                  .HasMaxLength(4000);
            });

            // ----- ActivityDate (Value Object) -----
            builder.OwnsOne(a => a.Date, vo =>
            {
                vo.Property(x => x.DateIso)
                  .HasColumnName("DateIso")
                  .IsRequired();
            });

            // ----- Address (Value Object) -----
            builder.OwnsOne(a => a.Address, vo =>
            {
                vo.Property(x => x.Street).HasColumnName("Address_Street").HasMaxLength(200).IsRequired();
                vo.Property(x => x.HouseNo).HasColumnName("Address_HouseNo").HasMaxLength(50).IsRequired();
                vo.Property(x => x.ZipCode).HasColumnName("Address_ZipCode").HasMaxLength(20);
                vo.Property(x => x.City).HasColumnName("Address_City").HasMaxLength(100).IsRequired();
                vo.Property(x => x.State).HasColumnName("Address_State").HasMaxLength(100);
                vo.Property(x => x.Country).HasColumnName("Address_Country").HasMaxLength(100).IsRequired();
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

            // ----- VideoUrl (Value Object) -----
            builder.OwnsOne(a => a.VideoUrl, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName("VideoUrl")
                  .HasMaxLength(500);
            });

            // ----- GalleryImages (collection Value Object) -----
            builder.OwnsOne(a => a.GalleryImages, vo =>
            {
                vo.OwnsMany(g => g.Images, img =>
                {
                    img.ToTable("ActivityGalleryImages");
                    img.WithOwner().HasForeignKey("ActivityId");
                    img.Property<int>("Id").ValueGeneratedOnAdd();
                    img.HasKey("Id");
                    img.Property(x => x.Value).HasColumnName("ImageUrl").HasMaxLength(500).IsRequired();
                });
            });

            // ----- IsActive -----
            builder.Property(a => a.IsActive)
                   .IsRequired();

            // ----- Auditable fields -----
            builder.Property(a => a.CreatedAt)
                   .IsRequired();
            builder.Property(a => a.UpdatedAt);
        }
    }
}
