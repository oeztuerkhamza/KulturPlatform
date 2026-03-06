using AutoMapper;
using KulturPlatform.Application.Commands.TeaEvent;
using KulturPlatform.Application.Dtos.TeaEventDto;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Application.Mappings
{
    public sealed class TeaEventMappingProfile : Profile
    {
        public TeaEventMappingProfile()
        {
            // DTO -> Create Command
            CreateMap<TeaEventDto, CreateTeaEventCommand>()
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => DateTime.Parse(src.Time)));

            // DTO -> Update Command
            CreateMap<TeaEventDto, UpdateTeaEventCommand>()
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => DateTime.Parse(src.Time)));

            // Create Command -> Entity
            CreateMap<CreateTeaEventCommand, TeaEvent>()
                .ConstructUsing(cmd => TeaEvent.CreateNew(
                    Title.Create(cmd.TitleTr),
                    Title.Create(cmd.TitleDe),
                    TeaEventContent.Create(
                        cmd.IntroTr,
                        cmd.IntroDe,
                        cmd.HeritageTextTr,
                        cmd.HeritageTextDe,
                        cmd.ParticipationTextTr,
                        cmd.ParticipationTextDe,
                        cmd.ContactEmail
                    ),
                    cmd.Date,
                    cmd.Time,
                    Location.Create(cmd.Location),
                    !string.IsNullOrWhiteSpace(cmd.ImageUrl) ? Url.Create(cmd.ImageUrl) : null,
                    null
                ));

            // Update Command -> Entity (Manual update)
            CreateMap<UpdateTeaEventCommand, TeaEvent>()
                .ConvertUsing((cmd, entity, context) =>
                {
                    if (entity == null) throw new ArgumentNullException(nameof(entity));

                    entity.UpdateTitle(
                        Title.Create(cmd.TitleTr),
                        Title.Create(cmd.TitleDe)
                    );

                    entity.UpdateContent(
                        TeaEventContent.Create(
                            cmd.IntroTr,
                            cmd.IntroDe,
                            cmd.HeritageTextTr,
                            cmd.HeritageTextDe,
                            cmd.ParticipationTextTr,
                            cmd.ParticipationTextDe,
                            cmd.ContactEmail
                        )
                    );

                    entity.Reschedule(cmd.Date, cmd.Time);

                    return entity;
                });

            // Entity -> DTO - Map hybrid image fields (Activity pattern)
            CreateMap<TeaEvent, TeaEventDto>()
                .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => src.TitleTurkish.Value))
                .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => src.TitleGerman.Value))
                .ForMember(dest => dest.IntroTr, opt => opt.MapFrom(src => src.Content.IntroTr))
                .ForMember(dest => dest.IntroDe, opt => opt.MapFrom(src => src.Content.IntroDe))
                .ForMember(dest => dest.HeritageTextTr, opt => opt.MapFrom(src => src.Content.HeritageTextTr))
                .ForMember(dest => dest.HeritageTextDe, opt => opt.MapFrom(src => src.Content.HeritageTextDe))
                .ForMember(dest => dest.ParticipationTextTr, opt => opt.MapFrom(src => src.Content.ParticipationTextTr))
                .ForMember(dest => dest.ParticipationTextDe, opt => opt.MapFrom(src => src.Content.ParticipationTextDe))
                .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.Content.ContactEmail))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location.Value))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl != null ? src.ImageUrl.Value : null)) // Deprecated
                .ForMember(dest => dest.ImageSource, opt => opt.MapFrom(src => src.GetImageSource())) // Unified source
                .ForMember(dest => dest.ImageMetadata, opt => opt.MapFrom(src => CreateImageMetadata(src))) // Metadata
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));
        }

        private static ImageMetadataDto? CreateImageMetadata(TeaEvent teaEvent)
        {
            if (teaEvent.ImageData != null)
            {
                return new ImageMetadataDto(
                    StorageType: "Database",
                    MimeType: teaEvent.ImageData.MimeType,
                    FileName: teaEvent.ImageData.FileName,
                    FileSizeKB: teaEvent.ImageData.FileSizeBytes / 1024
                );
            }
            else if (teaEvent.ImageUrl != null)
            {
                return new ImageMetadataDto(
                    StorageType: "URL",
                    MimeType: null,
                    FileName: null,
                    FileSizeKB: null
                );
            }

            return null;
        }
    }
}
/