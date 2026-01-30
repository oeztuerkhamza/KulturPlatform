using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates;

public class AboutUsWhoWeAre : AuditableEntity, IAggregateRoot
{
    public Description WhoWeAreTr { get; private set; }
    public Description WhoWeAreDe { get; private set; }
    
    // ? Hybrid banner/header image storage
    public Url? BannerImageUrl { get; private set; }
    public ImageData? BannerImageData { get; private set; }

    protected AboutUsWhoWeAre() { }

    private AboutUsWhoWeAre(
        Guid id, 
        Description whoWeAreTr, 
        Description whoWeAreDe,
        Url? bannerImageUrl,
        ImageData? bannerImageData) : base(id)
    {
        // Validate that only one image source is provided
        if (bannerImageUrl != null && bannerImageData != null)
            throw new ArgumentException("Cannot specify both BannerImageUrl and BannerImageData. Choose one image source.");

        WhoWeAreTr = whoWeAreTr;
        WhoWeAreDe = whoWeAreDe;
        BannerImageUrl = bannerImageUrl;
        BannerImageData = bannerImageData;
    }

    public static AboutUsWhoWeAre Create(
        Description whoWeAreTr, 
        Description whoWeAreDe,
        Url? bannerImageUrl = null,
        ImageData? bannerImageData = null)
    {
        return new AboutUsWhoWeAre(Guid.NewGuid(), whoWeAreTr, whoWeAreDe, bannerImageUrl, bannerImageData);
    }

    public void Update(
        Description whoWeAreTr, 
        Description whoWeAreDe,
        Url? bannerImageUrl = null,
        ImageData? bannerImageData = null)
    {
        // Validate that only one image source is provided
        if (bannerImageUrl != null && bannerImageData != null)
            throw new ArgumentException("Cannot specify both BannerImageUrl and BannerImageData. Choose one image source.");

        WhoWeAreTr = whoWeAreTr;
        WhoWeAreDe = whoWeAreDe;
        
        // Clear both first
        BannerImageUrl = null;
        BannerImageData = null;
        
        // Then set the appropriate one
        if (bannerImageData != null)
        {
            BannerImageData = bannerImageData;
        }
        else if (bannerImageUrl != null)
        {
            BannerImageUrl = bannerImageUrl;
        }
        
        SetUpdatedAt();
    }

    /// <summary>
    /// Gets the banner image source for frontend
    /// </summary>
    public string? GetBannerImageSource()
    {
        if (BannerImageData != null)
            return BannerImageData.GetDataUri();
        
        return BannerImageUrl?.Value;
    }

    /// <summary>
    /// Checks if banner image exists
    /// </summary>
    public bool HasBannerImage() => BannerImageUrl != null || BannerImageData != null;
}
