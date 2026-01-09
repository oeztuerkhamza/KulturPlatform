using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.AggregateRoot
{
    public class HomeAggregate : IAggregateRoot
    {
        public List<Activity> RecentActivities { get; private set; } = new();
        public List<Feature> Features { get; private set; } = new();
        public CtaSection? Cta { get; private set; }
        public List<InstagramPost> InstagramFeed { get; private set; } = new();

        public HomeAggregate(
            List<Activity> activities,
            List<Feature> features,
            CtaSection? cta,
            List<InstagramPost> instagramFeed
        )
        {
            RecentActivities = activities;
            Features = features;
            Cta = cta;
            InstagramFeed = instagramFeed;
        }

        public List<Activity> GetTopActivities(int count) => RecentActivities.Take(count).ToList();
        public List<InstagramPost> GetRecentInstagramPosts(int count) => InstagramFeed.Take(count).ToList();
    }
}
