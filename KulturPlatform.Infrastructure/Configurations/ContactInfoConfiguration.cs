using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.ToTable("ContactInfos");
            builder.HasKey(c => c.Id);

            // VO: Email & Phone (ValueConverter ile string olarak DB'ye yaz)
            var emailConverter = new ValueConverter<Email, string>(v => v.Value, v => new Email(v));
            var phoneConverter = new ValueConverter<PhoneNumber, string>(v => v.Value, v => new PhoneNumber(v));

            builder.Property(c => c.Email).HasConversion(emailConverter).HasMaxLength(200).IsRequired();
            builder.Property(c => c.Phone).HasConversion(phoneConverter).HasMaxLength(50);

            // VO: Street (Owned Entity)
            builder.OwnsOne(a => a.Address, vo =>
            {
                vo.Property(x => x.Street).HasColumnName("Location_Street").HasMaxLength(200).IsRequired();
                vo.Property(x => x.HouseNo).HasColumnName("Location_HouseNo").HasMaxLength(100).IsRequired();
                vo.Property(x => x.ZipCode).HasColumnName("Location_ZipCode").HasMaxLength(20).IsRequired();
                vo.Property(x => x.City).HasColumnName("Location_City").HasMaxLength(100).IsRequired();
                vo.Property(x => x.State).HasColumnName("Location_State").HasMaxLength(100);
                vo.Property(x => x.Country).HasColumnName("Location_Country").HasMaxLength(100).IsRequired();

            });

            // VO: SocialMediaLinks (Owned Entity)
            builder.OwnsOne(c => c.SocialMedia, sm =>
            {
                sm.Property(s => s.Facebook).HasMaxLength(200).HasColumnName("Facebook");
                sm.Property(s => s.Instagram).HasMaxLength(200).HasColumnName("Instagram");
                sm.Property(s => s.Twitter).HasMaxLength(200).HasColumnName("Twitter");
            });

            builder.Property(c => c.OfficeHours).HasMaxLength(200);
        }
    }
}
