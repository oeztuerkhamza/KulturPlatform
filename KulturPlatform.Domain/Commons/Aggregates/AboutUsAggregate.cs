using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.AggregateRoot;

public class AboutUsAggregate : IAggregateRoot
{
    public AboutUsQuote? Quote { get; private set; }
    public AboutUsWhoWeAre? WhoWeAre { get; private set; }
    public AboutUsGoals? Goals { get; private set; }
    public AboutUsVision? Vision { get; private set; }
    public AboutUsMission? Mission { get; private set; }
    public AboutUsHumanRights? HumanRights { get; private set; }
    public List<CoreValue> CoreValues { get; private set; } = new();
    public List<FocusArea> FocusAreas { get; private set; } = new();
    public List<ActivityArea> ActivityAreas { get; private set; } = new();
    public List<TeamMember> TeamMembers { get; private set; } = new();

    public AboutUsAggregate(
        AboutUsQuote? quote,
        AboutUsWhoWeAre? whoWeAre,
        AboutUsGoals? goals,
        AboutUsVision? vision,
        AboutUsMission? mission,
        AboutUsHumanRights? humanRights,
        List<CoreValue> coreValues,
        List<FocusArea> focusAreas,
        List<ActivityArea> activityAreas,
        List<TeamMember> teamMembers)
    {
        Quote = quote;
        WhoWeAre = whoWeAre;
        Goals = goals;
        Vision = vision;
        Mission = mission;
        HumanRights = humanRights;
        CoreValues = coreValues;
        FocusAreas = focusAreas;
        ActivityAreas = activityAreas;
        TeamMembers = teamMembers;
    }

    public List<CoreValue> GetOrderedCoreValues() => CoreValues.OrderBy(x => x.Order).ToList();
    public List<FocusArea> GetOrderedFocusAreas() => FocusAreas.OrderBy(x => x.Order).ToList();
    public List<ActivityArea> GetOrderedActivityAreas() => ActivityAreas.OrderBy(x => x.Order).ToList();
    public List<TeamMember> GetOrderedTeamMembers() => TeamMembers.OrderBy(x => x.Order).ToList();
}
