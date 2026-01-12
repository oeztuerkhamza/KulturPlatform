using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Dtos.SatzungDto;
using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Application.Mappings
{
    public class SatzungMappingProfile : Profile
    {
        public SatzungMappingProfile()
        {
            // VO Dönüşümleri
            CreateMap<SectionContentDto, SectionContent>()
                .ConvertUsing(src => SectionContent.Create(src.Heading, src.BodyTurkish, src.BodyGerman));

            CreateMap<TitleDto, Title>()
                .ConvertUsing(src => Title.Create(src.Value));

            CreateMap<PurposeDto, Purpose>()
                .ConvertUsing(src => Purpose.Create(src.Letter, SectionContent.Create(src.Content.Heading, src.Content.BodyTurkish, src.Content.BodyGerman)));

            CreateMap<MembershipDto, MembershipDetail>()
                .ConvertUsing<MembershipDtoToMembershipDetailConverter>();

            // 🔹 Domain Entity → DTO Mapping
            CreateMap<Domain.Commons.Aggregates.Satzung, SatzungDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TitleTurkish, opt => opt.MapFrom(src => src.TitleTurkish))
                .ForMember(dest => dest.TitleGerman, opt => opt.MapFrom(src => src.TitleGerman))
                .ForMember(dest => dest.NameAndSeatTurkish, opt => opt.MapFrom(src => src.NameAndSeatTurkish))
                .ForMember(dest => dest.NameAndSeatGerman, opt => opt.MapFrom(src => src.NameAndSeatGerman))
                .ForMember(dest => dest.NameDescTurkish, opt => opt.MapFrom(src => src.NameDescTurkish))
                .ForMember(dest => dest.NameDescGerman, opt => opt.MapFrom(src => src.NameDescGerman))
                .ForMember(dest => dest.SeatTurkish, opt => opt.MapFrom(src => src.SeatTurkish))
                .ForMember(dest => dest.SeatGerman, opt => opt.MapFrom(src => src.SeatGerman))
                .ForMember(dest => dest.SeatDescTurkish, opt => opt.MapFrom(src => src.SeatDescTurkish))
                .ForMember(dest => dest.SeatDescGerman, opt => opt.MapFrom(src => src.SeatDescGerman))
                .ForMember(dest => dest.FiscalYearTurkish, opt => opt.MapFrom(src => src.FiscalYearTurkish))
                .ForMember(dest => dest.FiscalYearGerman, opt => opt.MapFrom(src => src.FiscalYearGerman))
                .ForMember(dest => dest.FiscalYearDescTurkish, opt => opt.MapFrom(src => src.FiscalYearDescTurkish))
                .ForMember(dest => dest.FiscalYearDescGerman, opt => opt.MapFrom(src => src.FiscalYearDescGerman))
                .ForMember(dest => dest.PurposeOfAssociationTurkish, opt => opt.MapFrom(src => src.PurposeOfAssociationTurkish))
                .ForMember(dest => dest.PurposeOfAssociationGerman, opt => opt.MapFrom(src => src.PurposeOfAssociationGerman))
                .ForMember(dest => dest.Purposes, opt => opt.MapFrom(src => src.Purposes))
                .ForMember(dest => dest.GemeinnuetzigkeitTurkish, opt => opt.MapFrom(src => src.GemeinnuetzigkeitTurkish))
                .ForMember(dest => dest.GemeinnuetzigkeitGerman, opt => opt.MapFrom(src => src.GemeinnuetzigkeitGerman))
                .ForMember(dest => dest.PoliticalNeutralityTurkish, opt => opt.MapFrom(src => src.PoliticalNeutralityTurkish))
                .ForMember(dest => dest.PoliticalNeutralityGerman, opt => opt.MapFrom(src => src.PoliticalNeutralityGerman))
                .ForMember(dest => dest.Memberships, opt => opt.MapFrom(src => src.Memberships))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));
        }
    }

}
