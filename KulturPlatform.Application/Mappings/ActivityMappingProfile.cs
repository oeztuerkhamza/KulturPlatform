using AutoMapper;
using KulturPlatform.Application.Dtos.Activity;
using KulturPlatform.Application.Dtos.LocalizationDto;
using KulturPlatform.Domain.Commons.AggregateRoot;

namespace KulturPlatform.Application.Mappings
{
    public class ActivityMappingProfile : Profile
    {
        public ActivityMappingProfile()
        {
            CreateMap<Activity, ActivityDto>()
                .ConstructUsing(src => new ActivityDto(
                    src.Id,
                    src.TitleTr.Value,
                    src.TitleDe.Value,
                    src.DescriptionTr.Value,
                    src.DescriptionDe.Value,
                    src.DetailedContentTr != null ? src.DetailedContentTr.Value : null,
                    src.DetailedContentDe != null ? src.DetailedContentDe.Value : null,
                    src.Date.DateIso.ToString("yyyy-MM-dd"),
                    new AddressDto
                    {
                        Street = src.Address.Street,
                        HouseNo = src.Address.HouseNo,
                        ZipCode = src.Address.ZipCode,
                        City = src.Address.City,
                        State = src.Address.State,
                        Country = src.Address.Country
                    },
                    src.Category.Value,
                    src.ImageUrl != null ? src.ImageUrl.Value : null,
                    src.GalleryImages.Images.Select(img => img.Value).ToList(),
                    src.VideoUrl != null ? src.VideoUrl.Value : null,
                    src.IsActive
                ));
        }
    }
}
