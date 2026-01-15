using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates;

public class AboutUsHumanRights : AuditableEntity, IAggregateRoot
{
    public Title TitleTr { get; private set; }
    public Title TitleDe { get; private set; }
    public Description DescriptionTr { get; private set; }
    public Description DescriptionDe { get; private set; }
    public string TenkilMuseumUrl { get; private set; }
    public string InstagramUrl { get; private set; }

    protected AboutUsHumanRights() { }

    private AboutUsHumanRights(
        Guid id,
        Title titleTr,
        Title titleDe,
        Description descriptionTr,
        Description descriptionDe,
        string tenkilMuseumUrl,
        string instagramUrl) : base(id)
    {
        TitleTr = titleTr;
        TitleDe = titleDe;
        DescriptionTr = descriptionTr;
        DescriptionDe = descriptionDe;
        TenkilMuseumUrl = tenkilMuseumUrl;
        InstagramUrl = instagramUrl;
    }

    public static AboutUsHumanRights Create(
        Title titleTr,
        Title titleDe,
        Description descriptionTr,
        Description descriptionDe,
        string tenkilMuseumUrl,
        string instagramUrl)
    {
        return new AboutUsHumanRights(
            Guid.NewGuid(),
            titleTr,
            titleDe,
            descriptionTr,
            descriptionDe,
            tenkilMuseumUrl,
            instagramUrl);
    }

    public void Update(
        Title titleTr,
        Title titleDe,
        Description descriptionTr,
        Description descriptionDe,
        string tenkilMuseumUrl,
        string instagramUrl)
    {
        TitleTr = titleTr;
        TitleDe = titleDe;
        DescriptionTr = descriptionTr;
        DescriptionDe = descriptionDe;
        TenkilMuseumUrl = tenkilMuseumUrl;
        InstagramUrl = instagramUrl;
        SetUpdatedAt();
    }
}
