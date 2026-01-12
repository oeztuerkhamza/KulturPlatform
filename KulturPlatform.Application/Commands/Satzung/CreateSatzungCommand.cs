using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.Satzung
{
    public record CreateSatzungCommand(
        Title TitleTurkish,
        Title TitleGerman,
        SectionContent NameAndSeatTurkish,
        SectionContent NameAndSeatGerman,
        SectionContent NameDescTurkish,
        SectionContent NameDescGerman,
        SectionContent SeatTurkish,
        SectionContent SeatGerman,
        SectionContent SeatDescTurkish,
        SectionContent SeatDescGerman,
        SectionContent FiscalYearTurkish,
        SectionContent FiscalYearGerman,
        SectionContent FiscalYearDescTurkish,
        SectionContent FiscalYearDescGerman,
        SectionContent PurposeOfAssociationTurkish,
        SectionContent PurposeOfAssociationGerman,
        List<Purpose> Purposes,
        SectionContent GemeinnuetzigkeitTurkish,
        SectionContent GemeinnuetzigkeitGerman,
        SectionContent PoliticalNeutralityTurkish,
        SectionContent PoliticalNeutralityGerman,
        List<MembershipDetail> Memberships
    ) : IRequest<Guid>;

}
