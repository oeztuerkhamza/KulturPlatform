using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates;

public class AboutUs : AuditableEntity, IAggregateRoot
{
    public Description QuoteTr { get; private set; }
    public Description QuoteDe { get; private set; }
    public string QuoteAuthor { get; private set; }

    public Description WhoWeAreTr { get; private set; }
    public Description WhoWeAreDe { get; private set; }
    public Description GoalsTr { get; private set; }
    public Description GoalsDe { get; private set; }
    public Description VisionTr { get; private set; }
    public Description VisionDe { get; private set; }
    public Description MissionTr { get; private set; }
    public Description MissionDe { get; private set; }

    private readonly List<CoreValue> _coreValues = new();
    public IReadOnlyCollection<CoreValue> CoreValues => _coreValues.AsReadOnly();

    private readonly List<FocusArea> _focusAreas = new();
    public IReadOnlyCollection<FocusArea> FocusAreas => _focusAreas.AsReadOnly();

    private readonly List<ActivityArea> _activityAreas = new();
    public IReadOnlyCollection<ActivityArea> ActivityAreas => _activityAreas.AsReadOnly();

    private readonly List<TeamMember> _teamMembers = new();
    public IReadOnlyCollection<TeamMember> TeamMembers => _teamMembers.AsReadOnly();

    // EF Core için
    protected AboutUs() { }

    // Private constructor that sets Id
    private AboutUs(Guid id,
        Description quoteTr,
        Description quoteDe,
        string quoteAuthor,
        Description whoWeAreTr,
        Description whoWeAreDe,
        Description goalsTr,
        Description goalsDe,
        Description visionTr,
        Description visionDe,
        Description missionTr,
        Description missionDe,
        IEnumerable<CoreValue> coreValues,
        IEnumerable<FocusArea> focusAreas,
        IEnumerable<ActivityArea> activityAreas,
        IEnumerable<TeamMember> teamMembers) : base(id)
    {
        QuoteTr = quoteTr;
        QuoteDe = quoteDe;
        QuoteAuthor = quoteAuthor;
        WhoWeAreTr = whoWeAreTr;
        WhoWeAreDe = whoWeAreDe;
        GoalsTr = goalsTr;
        GoalsDe = goalsDe;
        VisionTr = visionTr;
        VisionDe = visionDe;
        MissionTr = missionTr;
        MissionDe = missionDe;

        _coreValues.AddRange(coreValues ?? Enumerable.Empty<CoreValue>());
        _focusAreas.AddRange(focusAreas ?? Enumerable.Empty<FocusArea>());
        _activityAreas.AddRange(activityAreas ?? Enumerable.Empty<ActivityArea>());
        _teamMembers.AddRange(teamMembers ?? Enumerable.Empty<TeamMember>());
    }

    // Domain factory
    public static AboutUs Create(
        Description quoteTr,
        Description quoteDe,
        string quoteAuthor,
        Description whoWeAreTr,
        Description whoWeAreDe,
        Description goalsTr,
        Description goalsDe,
        Description visionTr,
        Description visionDe,
        Description missionTr,
        Description missionDe,
        IEnumerable<CoreValue> coreValues,
        IEnumerable<FocusArea> focusAreas,
        IEnumerable<ActivityArea> activityAreas,
        IEnumerable<TeamMember> teamMembers)
    {
        if (string.IsNullOrWhiteSpace(quoteAuthor))
            throw new ArgumentException("Quote author boş olamaz");

        return new AboutUs(Guid.NewGuid(), quoteTr, quoteDe, quoteAuthor, whoWeAreTr, whoWeAreDe, goalsTr, goalsDe, visionTr, visionDe, missionTr, missionDe, coreValues, focusAreas, activityAreas, teamMembers);
    }

    public void Update(
        Description quoteTr,
        Description quoteDe,
        string quoteAuthor,
        Description whoWeAreTr,
        Description whoWeAreDe,
        Description goalsTr,
        Description goalsDe,
        Description visionTr,
        Description visionDe,
        Description missionTr,
        Description missionDe,
        IEnumerable<CoreValue> coreValues,
        IEnumerable<FocusArea> focusAreas,
        IEnumerable<ActivityArea> activityAreas,
        IEnumerable<TeamMember> teamMembers)
    {
        QuoteTr = quoteTr;
        QuoteDe = quoteDe;
        QuoteAuthor = quoteAuthor;
        WhoWeAreTr = whoWeAreTr;
        WhoWeAreDe = whoWeAreDe;
        GoalsTr = goalsTr;
        GoalsDe = goalsDe;
        VisionTr = visionTr;
        VisionDe = visionDe;
        MissionTr = missionTr;
        MissionDe = missionDe;

        _coreValues.Clear();
        _coreValues.AddRange(coreValues ?? Enumerable.Empty<CoreValue>());

        _focusAreas.Clear();
        _focusAreas.AddRange(focusAreas ?? Enumerable.Empty<FocusArea>());

        _activityAreas.Clear();
        _activityAreas.AddRange(activityAreas ?? Enumerable.Empty<ActivityArea>());

        _teamMembers.Clear();
        _teamMembers.AddRange(teamMembers ?? Enumerable.Empty<TeamMember>());

        SetUpdatedAt();
    }
}
