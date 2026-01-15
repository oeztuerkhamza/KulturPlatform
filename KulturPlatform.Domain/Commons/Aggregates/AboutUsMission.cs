using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates;

public class AboutUsMission : AuditableEntity, IAggregateRoot
{
    public Description MissionTr { get; private set; }
    public Description MissionDe { get; private set; }

    protected AboutUsMission() { }

    private AboutUsMission(Guid id, Description missionTr, Description missionDe) : base(id)
    {
        MissionTr = missionTr;
        MissionDe = missionDe;
    }

    public static AboutUsMission Create(Description missionTr, Description missionDe)
    {
        return new AboutUsMission(Guid.NewGuid(), missionTr, missionDe);
    }

    public void Update(Description missionTr, Description missionDe)
    {
        MissionTr = missionTr;
        MissionDe = missionDe;
        SetUpdatedAt();
    }
}
