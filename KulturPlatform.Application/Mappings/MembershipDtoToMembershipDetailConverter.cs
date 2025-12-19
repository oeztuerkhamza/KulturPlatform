using AutoMapper;
using KulturPlatform.Application.Dtos.SatzungDto;
using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Application.Mappings
{
    public class MembershipDtoToMembershipDetailConverter : ITypeConverter<MembershipDto, MembershipDetail>
    {
        public MembershipDetail Convert(MembershipDto source, MembershipDetail destination, ResolutionContext context)
        {
            var descTurkish = SectionContent.Create(
                source.DescriptionTurkish.Heading,
                source.DescriptionTurkish.BodyTurkish,
                source.DescriptionTurkish.BodyGerman
            );

            var descGerman = SectionContent.Create(
                source.DescriptionGerman.Heading,
                source.DescriptionGerman.BodyTurkish,
                source.DescriptionGerman.BodyGerman
            );
            var type = SectionContent.Create(
                source.Type.Heading,
                source.Type.BodyTurkish,
                source.Type.BodyGerman
            );

            return MembershipDetail.Create(type, descTurkish, descGerman);
        }
    }


}
