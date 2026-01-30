namespace KulturPlatform.Application.Dtos.AboutUs;

public class AboutUsQuoteDto
{
    public Guid Id { get; set; }
    public string QuoteTr { get; set; }
    public string QuoteDe { get; set; }
    public string QuoteAuthor { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class AboutUsWhoWeAreDto
{
    public Guid Id { get; set; }
    public string WhoWeAreTr { get; set; }
    public string WhoWeAreDe { get; set; }
    
    /// <summary>
    /// Banner image source - either URL or data URI (base64)
    /// </summary>
    public string? BannerImageSource { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class AboutUsGoalsDto
{
    public Guid Id { get; set; }
    public string GoalsTr { get; set; }
    public string GoalsDe { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class AboutUsVisionDto
{
    public Guid Id { get; set; }
    public string VisionTr { get; set; }
    public string VisionDe { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class AboutUsMissionDto
{
    public Guid Id { get; set; }
    public string MissionTr { get; set; }
    public string MissionDe { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CoreValueDto
{
    public Guid Id { get; set; }
    public string TitleTr { get; set; }
    public string TitleDe { get; set; }
    public string DescriptionTr { get; set; }
    public string DescriptionDe { get; set; }
    public int Order { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class FocusAreaDto
{
    public Guid Id { get; set; }
    public string TitleTr { get; set; }
    public string TitleDe { get; set; }
    public string DescriptionTr { get; set; }
    public string DescriptionDe { get; set; }
    
    /// <summary>
    /// Icon source - either URL or data URI (base64)
    /// </summary>
    public string? IconSource { get; set; }
    
    public int Order { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class ActivityAreaDto
{
    public Guid Id { get; set; }
    public string TitleTr { get; set; }
    public string TitleDe { get; set; }
    public string DescriptionTr { get; set; }
    public string DescriptionDe { get; set; }
    public int Order { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class AboutUsHumanRightsDto
{
    public Guid Id { get; set; }
    public string TitleTr { get; set; }
    public string TitleDe { get; set; }
    public string DescriptionTr { get; set; }
    public string DescriptionDe { get; set; }
    public string TenkilMuseumUrl { get; set; }
    public string InstagramUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class AboutUsAggregateDto
{
    public AboutUsQuoteDto? Quote { get; set; }
    public AboutUsWhoWeAreDto? WhoWeAre { get; set; }
    public AboutUsGoalsDto? Goals { get; set; }
    public AboutUsVisionDto? Vision { get; set; }
    public AboutUsMissionDto? Mission { get; set; }
    public AboutUsHumanRightsDto? HumanRights { get; set; }
    public List<CoreValueDto> CoreValues { get; set; } = new();
    public List<FocusAreaDto> FocusAreas { get; set; } = new();
    public List<ActivityAreaDto> ActivityAreas { get; set; } = new();
    public List<TeamMemberDto> TeamMembers { get; set; } = new();
}
