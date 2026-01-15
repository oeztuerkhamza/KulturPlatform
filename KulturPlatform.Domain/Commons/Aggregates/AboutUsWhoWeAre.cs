using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates;

public class AboutUsWhoWeAre : AuditableEntity, IAggregateRoot
{
    public Description WhoWeAreTr { get; private set; }
    public Description WhoWeAreDe { get; private set; }

    protected AboutUsWhoWeAre() { }

    private AboutUsWhoWeAre(Guid id, Description whoWeAreTr, Description whoWeAreDe) : base(id)
    {
        WhoWeAreTr = whoWeAreTr;
        WhoWeAreDe = whoWeAreDe;
    }

    public static AboutUsWhoWeAre Create(Description whoWeAreTr, Description whoWeAreDe)
    {
        return new AboutUsWhoWeAre(Guid.NewGuid(), whoWeAreTr, whoWeAreDe);
    }

    public void Update(Description whoWeAreTr, Description whoWeAreDe)
    {
        WhoWeAreTr = whoWeAreTr;
        WhoWeAreDe = whoWeAreDe;
        SetUpdatedAt();
    }
}
