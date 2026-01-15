using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates;

public class AboutUsGoals : AuditableEntity, IAggregateRoot
{
    public Description GoalsTr { get; private set; }
    public Description GoalsDe { get; private set; }

    protected AboutUsGoals() { }

    private AboutUsGoals(Guid id, Description goalsTr, Description goalsDe) : base(id)
    {
        GoalsTr = goalsTr;
        GoalsDe = goalsDe;
    }

    public static AboutUsGoals Create(Description goalsTr, Description goalsDe)
    {
        return new AboutUsGoals(Guid.NewGuid(), goalsTr, goalsDe);
    }

    public void Update(Description goalsTr, Description goalsDe)
    {
        GoalsTr = goalsTr;
        GoalsDe = goalsDe;
        SetUpdatedAt();
    }
}
