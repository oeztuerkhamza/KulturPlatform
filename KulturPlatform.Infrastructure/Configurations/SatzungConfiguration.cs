using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace KulturPlatform.Infrastructure.Configurations
{
    public class SatzungConfiguration : IEntityTypeConfiguration<Satzung>
    {
        public void Configure(EntityTypeBuilder<Satzung> builder)
        {
            builder.ToTable("Satzungs");
            builder.HasKey(x => x.Id);

            // Key
            builder.Property(x => x.Key)
                   .HasMaxLength(100)
                   .IsRequired();

            // Titles
            ConfigureTitle(builder, x => x.TitleTurkish, "TitleTurkish");
            ConfigureTitle(builder, x => x.TitleGerman, "TitleGerman");

            // SectionContents
            ConfigureSectionContents(builder);

            // Purposes
            builder.OwnsMany(x => x.Purposes, p =>
            {
                p.WithOwner().HasForeignKey("SatzungId");
                p.Property(x => x.Letter).HasColumnName("Letter").HasMaxLength(5).IsRequired();

                // Purpose Content (SectionContent)
                p.OwnsOne(x => x.Content, c =>
                {
                    c.Property(cc => cc.Heading)
                     .HasColumnName("Content_Heading")
                     .HasMaxLength(200)
                     .IsRequired();
                    c.Property(cc => cc.BodyTurkish)
                     .HasColumnName("Content_BodyTurkish")
                     .HasMaxLength(2000)
                     .IsRequired();
                    c.Property(cc => cc.BodyGerman)
                     .HasColumnName("Content_BodyGerman")
                     .HasMaxLength(2000)
                     .IsRequired();
                });

                p.ToTable("SatzungPurposes");
            });

            // Memberships
            builder.OwnsMany(x => x.Memberships, m =>
            {
                m.WithOwner().HasForeignKey("SatzungId");

                // Type as SectionContent
                m.OwnsOne(x => x.Type, sc =>
                {
                    sc.Property(x => x.Heading).HasColumnName("Type_Heading").HasMaxLength(200).IsRequired();
                    sc.Property(x => x.BodyTurkish).HasColumnName("Type_BodyTurkish").HasMaxLength(2000).IsRequired();
                    sc.Property(x => x.BodyGerman).HasColumnName("Type_BodyGerman").HasMaxLength(2000).IsRequired();
                });

                // DescriptionTurkish as SectionContent
                m.OwnsOne(x => x.DescriptionTurkish, sc =>
                {
                    sc.Property(x => x.Heading).HasColumnName("DescriptionTurkish_Heading").HasMaxLength(200).IsRequired();
                    sc.Property(x => x.BodyTurkish).HasColumnName("DescriptionTurkish_BodyTurkish").HasMaxLength(2000).IsRequired();
                    sc.Property(x => x.BodyGerman).HasColumnName("DescriptionTurkish_BodyGerman").HasMaxLength(2000).IsRequired();
                });

                // DescriptionGerman as SectionContent
                m.OwnsOne(x => x.DescriptionGerman, sc =>
                {
                    sc.Property(x => x.Heading).HasColumnName("DescriptionGerman_Heading").HasMaxLength(200).IsRequired();
                    sc.Property(x => x.BodyTurkish).HasColumnName("DescriptionGerman_BodyTurkish").HasMaxLength(2000).IsRequired();
                    sc.Property(x => x.BodyGerman).HasColumnName("DescriptionGerman_BodyGerman").HasMaxLength(2000).IsRequired();
                });

                m.ToTable("SatzungMemberships");
            });

            // Audit fields
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);

            // Index
            builder.HasIndex(x => x.Key).IsUnique();
        }

        // Generic Title configuration
        private void ConfigureTitle<T>(
            EntityTypeBuilder<T> builder,
            Expression<Func<T, Title>> propertyExpression,
            string columnName) where T : class
        {
            builder.OwnsOne(propertyExpression, vo =>
            {
                vo.Property(x => x.Value)
                  .HasColumnName(columnName)
                  .HasMaxLength(300)
                  .IsRequired();
            });
        }

        // Map all SectionContents in Satzung (main table)
        private void ConfigureSectionContents(EntityTypeBuilder<Satzung> builder)
        {
            var sectionContents = new (Expression<Func<Satzung, SectionContent>> Expr, string Column)[]
            {
                (x => x.NameAndSeatTurkish, "NameAndSeatTurkish"),
                (x => x.NameAndSeatGerman, "NameAndSeatGerman"),
                (x => x.NameDescTurkish, "NameDescTurkish"),
                (x => x.NameDescGerman, "NameDescGerman"),
                (x => x.SeatTurkish, "SeatTurkish"),
                (x => x.SeatGerman, "SeatGerman"),
                (x => x.SeatDescTurkish, "SeatDescTurkish"),
                (x => x.SeatDescGerman, "SeatDescGerman"),
                (x => x.FiscalYearTurkish, "FiscalYearTurkish"),
                (x => x.FiscalYearGerman, "FiscalYearGerman"),
                (x => x.FiscalYearDescTurkish, "FiscalYearDescTurkish"),
                (x => x.FiscalYearDescGerman, "FiscalYearDescGerman"),
                (x => x.PurposeOfAssociationTurkish, "PurposeOfAssociationTurkish"),
                (x => x.PurposeOfAssociationGerman, "PurposeOfAssociationGerman"),
                (x => x.GemeinnuetzigkeitTurkish, "GemeinnuetzigkeitTurkish"),
                (x => x.GemeinnuetzigkeitGerman, "GemeinnuetzigkeitGerman"),
                (x => x.PoliticalNeutralityTurkish, "PoliticalNeutralityTurkish"),
                (x => x.PoliticalNeutralityGerman, "PoliticalNeutralityGerman")
            };

            foreach (var (expr, column) in sectionContents)
            {
                builder.OwnsOne(expr, vo =>
                {
                    vo.Property(x => x.Heading)
                        .HasColumnName($"{column}_Heading")
                        .HasMaxLength(200)
                        .IsRequired();

                    vo.Property(x => x.BodyTurkish)
                        .HasColumnName($"{column}_BodyTurkish")
                        .HasMaxLength(2000)
                        .IsRequired();

                    vo.Property(x => x.BodyGerman)
                        .HasColumnName($"{column}_BodyGerman")
                        .HasMaxLength(2000)
                        .IsRequired();
                });
            }
        }
    }
}
