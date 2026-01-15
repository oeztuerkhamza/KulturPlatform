using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Commons.Entities;

namespace KulturPlatform.Application.Mappings;

public class AboutUsAggregateProfile : Profile
{
    public AboutUsAggregateProfile()
    {
        CreateMap<AboutUsQuote, AboutUsQuoteDto>()
            .ForMember(dest => dest.QuoteTr, opt => opt.MapFrom(src => src.QuoteTr.Value))
            .ForMember(dest => dest.QuoteDe, opt => opt.MapFrom(src => src.QuoteDe.Value));

        CreateMap<AboutUsWhoWeAre, AboutUsWhoWeAreDto>()
            .ForMember(dest => dest.WhoWeAreTr, opt => opt.MapFrom(src => src.WhoWeAreTr.Value))
            .ForMember(dest => dest.WhoWeAreDe, opt => opt.MapFrom(src => src.WhoWeAreDe.Value));

        CreateMap<AboutUsGoals, AboutUsGoalsDto>()
            .ForMember(dest => dest.GoalsTr, opt => opt.MapFrom(src => src.GoalsTr.Value))
            .ForMember(dest => dest.GoalsDe, opt => opt.MapFrom(src => src.GoalsDe.Value));

        CreateMap<AboutUsVision, AboutUsVisionDto>()
            .ForMember(dest => dest.VisionTr, opt => opt.MapFrom(src => src.VisionTr.Value))
            .ForMember(dest => dest.VisionDe, opt => opt.MapFrom(src => src.VisionDe.Value));

        CreateMap<AboutUsMission, AboutUsMissionDto>()
            .ForMember(dest => dest.MissionTr, opt => opt.MapFrom(src => src.MissionTr.Value))
            .ForMember(dest => dest.MissionDe, opt => opt.MapFrom(src => src.MissionDe.Value));

        CreateMap<AboutUsHumanRights, AboutUsHumanRightsDto>()
            .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => src.TitleTr.Value))
            .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => src.TitleDe.Value))
            .ForMember(dest => dest.DescriptionTr, opt => opt.MapFrom(src => src.DescriptionTr.Value))
            .ForMember(dest => dest.DescriptionDe, opt => opt.MapFrom(src => src.DescriptionDe.Value));

        CreateMap<CoreValue, CoreValueDto>()
            .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => src.TitleTr.Value))
            .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => src.TitleDe.Value))
            .ForMember(dest => dest.DescriptionTr, opt => opt.MapFrom(src => src.DescriptionTr.Value))
            .ForMember(dest => dest.DescriptionDe, opt => opt.MapFrom(src => src.DescriptionDe.Value));

        CreateMap<FocusArea, FocusAreaDto>()
            .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => src.TitleTr.Value))
            .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => src.TitleDe.Value))
            .ForMember(dest => dest.DescriptionTr, opt => opt.MapFrom(src => src.DescriptionTr.Value))
            .ForMember(dest => dest.DescriptionDe, opt => opt.MapFrom(src => src.DescriptionDe.Value));

        CreateMap<ActivityArea, ActivityAreaDto>()
            .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => src.TitleTr.Value))
            .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => src.TitleDe.Value))
            .ForMember(dest => dest.DescriptionTr, opt => opt.MapFrom(src => src.DescriptionTr.Value))
            .ForMember(dest => dest.DescriptionDe, opt => opt.MapFrom(src => src.DescriptionDe.Value));

        CreateMap<TeamMember, TeamMemberDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new NameDto { Value = src.Name.Value }))
            .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => new TitleDto { Value = src.TitleTr.Value }))
            .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => new TitleDto { Value = src.TitleDe.Value }))
            .ForMember(dest => dest.DescriptionTr, opt => opt.MapFrom(src => src.DescriptionTr != null ? new DescriptionDto { Value = src.DescriptionTr.Value } : null))
            .ForMember(dest => dest.DescriptionDe, opt => opt.MapFrom(src => src.DescriptionDe != null ? new DescriptionDto { Value = src.DescriptionDe.Value } : null));

        CreateMap<AboutUsAggregate, AboutUsAggregateDto>();
    }
}
