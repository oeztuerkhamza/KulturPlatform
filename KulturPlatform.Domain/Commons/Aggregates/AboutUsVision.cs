using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates;

public class AboutUsVision : AuditableEntity, IAggregateRoot
{
    public Description VisionTr { get; private set; }
    public Description VisionDe { get; private set; }

    protected AboutUsVision() { }

    private AboutUsVision(Guid id, Description visionTr, Description visionDe) : base(id)
    {
        VisionTr = visionTr;
        VisionDe = visionDe;
    }

    public static AboutUsVision Create(Description visionTr, Description visionDe)
    {
        return new AboutUsVision(Guid.NewGuid(), visionTr, visionDe);
    }

    public void Update(Description visionTr, Description visionDe)
    {
        VisionTr = visionTr;
        VisionDe = visionDe;
        SetUpdatedAt();
    }
}
