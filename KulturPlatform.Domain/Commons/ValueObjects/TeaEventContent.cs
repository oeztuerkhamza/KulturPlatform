namespace KulturPlatform.Domain.Commons.ValueObjects;

public sealed record TeaEventContent
{
    public string IntroTr { get; }
    public string IntroDe { get; }

    public string HeritageTextTr { get; }
    public string HeritageTextDe { get; }

    public string ParticipationTextTr { get; }
    public string ParticipationTextDe { get; }

    public string ContactEmail { get; }

    private TeaEventContent(
        string introTr,
        string introDe,
        string heritageTextTr,
        string heritageTextDe,
        string participationTextTr,
        string participationTextDe,
        string contactEmail)
    {
        IntroTr = introTr;
        IntroDe = introDe;
        HeritageTextTr = heritageTextTr;
        HeritageTextDe = heritageTextDe;
        ParticipationTextTr = participationTextTr;
        ParticipationTextDe = participationTextDe;
        ContactEmail = contactEmail;
    }

    public static TeaEventContent Create(
        string introTr,
        string introDe,
        string heritageTextTr,
        string heritageTextDe,
        string participationTextTr,
        string participationTextDe,
        string contactEmail)
    {
        if (!contactEmail.Contains("@"))
            throw new ArgumentException("Invalid email");

        return new TeaEventContent(
            introTr, introDe,
            heritageTextTr, heritageTextDe,
            participationTextTr, participationTextDe,
            contactEmail
        );
    }
}