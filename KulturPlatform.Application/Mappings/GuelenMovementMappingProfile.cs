using AutoMapper;
using KulturPlatform.Application.Commands.GuelenMovement;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Application.Mappings
{
    public class GuelenMovementMappingProfile : Profile
    {
        public GuelenMovementMappingProfile()
        {
            CreateMap<GuelenMovement, Dtos.GuelenMovementDto>().ReverseMap();
            CreateMap<CreateGuelenMovementCommand, GuelenMovement>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<UpdateGuelenMovementCommand, GuelenMovement>();

        }
    }
}
