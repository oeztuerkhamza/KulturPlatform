using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<ValueItem> ValueItems { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<VolunteerSubmission> VolunteerSubmissions { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<PageContent> PageContents { get; set; }
        public DbSet<DonatePage> DonatePages { get; set; }
        public DbSet<Satzung> Satzungen { get; set; }
        public DbSet<GuelenMovement> GuelenMovements { get; set; }
        public DbSet<TeaEvent> TeaEvents { get; set; }
        public DbSet<LocalizationResource> LocalizationResources { get; set; }
        public DbSet<Imprint> Imprints { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }

        // About Us - New Structure
        public DbSet<AboutUsQuote> AboutUsQuotes { get; set; }
        public DbSet<AboutUsWhoWeAre> AboutUsWhoWeAre { get; set; }
        public DbSet<AboutUsGoals> AboutUsGoals { get; set; }
        public DbSet<AboutUsVision> AboutUsVision { get; set; }
        public DbSet<AboutUsMission> AboutUsMission { get; set; }
        public DbSet<AboutUsHumanRights> AboutUsHumanRights { get; set; }
        public DbSet<CoreValue> CoreValues { get; set; }
        public DbSet<FocusArea> FocusAreas { get; set; }
        public DbSet<ActivityArea> ActivityAreas { get; set; }

        // Home Page Aggregates
        public DbSet<HeroSection> HeroSections { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<CtaSection> CtaSections { get; set; }
        public DbSet<InstagramPost> InstagramPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }

    }
}
