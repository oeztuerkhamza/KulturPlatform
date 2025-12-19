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
        public DbSet<AboutUs> AboutUsEntities { get; set; }

        // New typed DbSets for AboutUs items
        public DbSet<CoreValue> AboutUsCoreValues { get; set; }
        public DbSet<FocusArea> AboutUsFocusAreas { get; set; }
        public DbSet<ActivityArea> AboutUsActivityAreas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }

    }
}
