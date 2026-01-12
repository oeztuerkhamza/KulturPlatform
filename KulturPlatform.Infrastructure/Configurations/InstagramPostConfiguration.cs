using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Commons.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class InstagramPostConfiguration : IEntityTypeConfiguration<InstagramPost>
    {
        public void Configure(EntityTypeBuilder<InstagramPost> builder)
        {
            builder.ToTable("InstagramPosts");
            builder.HasKey(i => i.Id);

            var urlConverter = new ValueConverter<Url, string>(v => v.Value, v => Url.Create(v));

            builder.Property(i => i.ImageUrl)
                .HasConversion(urlConverter)
                .HasColumnName("ImageUrl")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(i => i.Link)
                .HasConversion(urlConverter)
                .HasColumnName("Link")
                .HasMaxLength(500);

            builder.Property(i => i.CreatedAt).IsRequired();
            builder.Property(i => i.UpdatedAt);
        }
    }
}
