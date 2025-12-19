using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(c => c.Id);

            // ----- Title (Title VO) -----
            builder.OwnsOne(c => c.TitleTr, vo =>
            {
                vo.Property(t => t.Value)
                  .HasColumnName("TitleTr")
                  .IsRequired()
                  .HasMaxLength(200);
            });
            builder.OwnsOne(c => c.TitleDe, vo =>
            {
                vo.Property(t => t.Value)
                    .HasColumnName("TitleDe")
                    .IsRequired()
                    .HasMaxLength(200);
            });

            // ----- Description (Description VO) -----
            builder.OwnsOne(c => c.DescriptionTr, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("DescriptionTr")
                  .IsRequired()
                  .HasMaxLength(1000);
            });
            builder.OwnsOne(c => c.DescriptionDe, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("DescriptionDe")
                  .IsRequired()
                  .HasMaxLength(1000);
            });

            // ----- Details (CourseDetails VO) -----
            builder.OwnsOne(c => c.DetailsTr, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("DetailsTr")
                  .HasMaxLength(2000);
            });
            builder.OwnsOne(c => c.DetailsDe, vo =>
            {
                vo.Property(d => d.Value)
                  .HasColumnName("DetailsDe")
                  .HasMaxLength(2000);
            });

            // ----- Schedule (CourseSchedule VO) -----
            builder.OwnsOne(c => c.ScheduleTr, vo =>
            {
                vo.Property(s => s.Value)
                  .HasColumnName("ScheduleTr")
                  .HasMaxLength(2000);
            });
            builder.OwnsOne(c => c.ScheduleDe, vo =>
            {
                vo.Property(s => s.Value)
                  .HasColumnName("ScheduleDe")
                  .HasMaxLength(2000);
            });

            // ----- Icon -----
            builder.Property(c => c.Icon)
                 .HasMaxLength(500);

            // ----- Instructor -----
            builder.OwnsOne(c => c.Instructor, vo =>
            {
                vo.Property(i => i.Value)
                  .HasColumnName("Instructor")
                  .HasMaxLength(100);
            });

            // ----- Date -----
            builder.Property(c => c.Date);

            // ----- Address (Address VO) -----
            builder.OwnsOne(c => c.CourseLocation, vo =>
            {
                vo.Property(l => l.Street).HasColumnName("CourseLocation_Address").HasMaxLength(200);
                vo.Property(l => l.City).HasColumnName("CourseLocation_City").HasMaxLength(100);
                vo.Property(l => l.State).HasColumnName("CourseLocation_State").HasMaxLength(100);
                vo.Property(l => l.Country).HasColumnName("CourseLocation_Country").HasMaxLength(100);
                vo.Property(l => l.ZipCode).HasColumnName("CourseLocation_ZipCode").HasMaxLength(20);
            });

            // ----- Category (Category VO) -----
            builder.OwnsOne(c => c.CourseCategory, vo =>
            {
                vo.Property(cat => cat.Value)
                  .HasColumnName("CourseCategory")
                  .HasMaxLength(100);
            });

            // ----- IsActive -----
            builder.Property(c => c.IsActive)
                 .IsRequired();

            // ----- Audit fields -----
            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.UpdatedAt);
        }
    }
}
