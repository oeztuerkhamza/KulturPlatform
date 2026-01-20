using KulturPlatform.Application.Dtos.Activity;
using KulturPlatform.Application.Dtos.LocalizationDto;
using KulturPlatform.Application.Interfaces.Activity;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class ActivityReadService : IActivityReadService
    {
        private readonly AppDbContext _context;

        public ActivityReadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ActivityDto>> GetAllAsync()
        {
            return await _context.Activities
                .AsNoTracking()
                .Select(a => new ActivityDto(
                    a.Id,
                    a.TitleTr.Value,
                    a.TitleDe.Value,
                    a.DescriptionTr.Value,
                    a.DescriptionDe.Value,
                    a.DetailedContentTr != null ? a.DetailedContentTr.Value : null,
                    a.DetailedContentDe != null ? a.DetailedContentDe.Value : null,
                    a.Date.DateIso.ToString("yyyy-MM-dd"),
                    new AddressDto
                    {
                        Street = a.Address.Street,
                        HouseNo = a.Address.HouseNo,
                        ZipCode = a.Address.ZipCode,
                        City = a.Address.City,
                        State = a.Address.State,
                        Country = a.Address.Country
                    },
                    a.Category.Value,
                    a.ImageUrl != null ? a.ImageUrl.Value : null,
                    a.ImageData != null ? a.ImageData.GetDataUri() : (a.ImageUrl != null ? a.ImageUrl.Value : null),
                    CreateImageMetadata(a),
                    ConvertGalleryImages(a.GalleryImages.Images.Select(img => img.GetImageSource())),
                    a.VideoUrl != null ? a.VideoUrl.Value : null,
                    a.IsActive
                ))
                .ToListAsync();
        }

        public async Task<IEnumerable<ActivityDto>> GetUpcomingAsync(CancellationToken cancellationToken)
        {
            return await _context.Activities
                .AsNoTracking()
                .Where(a => a.IsActive && a.Date.DateIso >= DateTime.UtcNow)
                .OrderBy(a => a.Date.DateIso)
                .Select(a => new ActivityDto(
                    a.Id,
                    a.TitleTr.Value,
                    a.TitleDe.Value,
                    a.DescriptionTr.Value,
                    a.DescriptionDe.Value,
                    a.DetailedContentTr != null ? a.DetailedContentTr.Value : null,
                    a.DetailedContentDe != null ? a.DetailedContentDe.Value : null,
                    a.Date.DateIso.ToString("yyyy-MM-dd"),
                    new AddressDto
                    {
                        Street = a.Address.Street,
                        HouseNo = a.Address.HouseNo,
                        ZipCode = a.Address.ZipCode,
                        City = a.Address.City,
                        State = a.Address.State,
                        Country = a.Address.Country
                    },
                    a.Category.Value,
                    a.ImageUrl != null ? a.ImageUrl.Value : null,
                    a.ImageData != null ? a.ImageData.GetDataUri() : (a.ImageUrl != null ? a.ImageUrl.Value : null),
                    CreateImageMetadata(a),
                    ConvertGalleryImages(a.GalleryImages.Images.Select(img => img.GetImageSource())),
                    a.VideoUrl != null ? a.VideoUrl.Value : null,
                    a.IsActive
                ))
                .ToListAsync();
        }

        public async Task<ActivityDto?> GetByIdAsync(Guid id)
        {
            return await _context.Activities
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new ActivityDto(
                    a.Id,
                    a.TitleTr.Value,
                    a.TitleDe.Value,
                    a.DescriptionTr.Value,
                    a.DescriptionDe.Value,
                    a.DetailedContentTr != null ? a.DetailedContentTr.Value : null,
                    a.DetailedContentDe != null ? a.DetailedContentDe.Value : null,
                    a.Date.DateIso.ToString("yyyy-MM-dd"),
                    new AddressDto
                    {
                        Street = a.Address.Street,
                        HouseNo = a.Address.HouseNo,
                        ZipCode = a.Address.ZipCode,
                        City = a.Address.City,
                        State = a.Address.State,
                        Country = a.Address.Country
                    },
                    a.Category.Value,
                    a.ImageUrl != null ? a.ImageUrl.Value : null,
                    a.ImageData != null ? a.ImageData.GetDataUri() : (a.ImageUrl != null ? a.ImageUrl.Value : null),
                    CreateImageMetadata(a),
                    ConvertGalleryImages(a.GalleryImages.Images.Select(img => img.GetImageSource())),
                    a.VideoUrl != null ? a.VideoUrl.Value : null,
                    a.IsActive
                ))
                .FirstOrDefaultAsync();
        }

        private static ImageMetadataDto? CreateImageMetadata(Domain.Commons.AggregateRoot.Activity activity)
        {
            if (activity.ImageData != null)
            {
                return new ImageMetadataDto(
                    StorageType: "Database",
                    MimeType: activity.ImageData.MimeType,
                    FileName: activity.ImageData.FileName,
                    FileSizeKB: activity.ImageData.FileSizeBytes / 1024
                );
            }
            else if (activity.ImageUrl != null)
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

        /// <summary>
        /// Converts raw image sources to GalleryImageDto objects
        /// </summary>
        private static List<GalleryImageDto> ConvertGalleryImages(IEnumerable<string> rawImages)
        {
            return rawImages.Select(ConvertGalleryImage).ToList();
        }

        /// <summary>
        /// Converts a single raw image string to GalleryImageDto
        /// </summary>
        private static GalleryImageDto ConvertGalleryImage(string rawImage)
        {
            if (string.IsNullOrEmpty(rawImage))
                return new GalleryImageDto { Url = null, Base64Data = null, FileName = null };

            // If it's a URL (doesn't start with data:)
            if (!rawImage.StartsWith("data:"))
                return new GalleryImageDto { Url = rawImage, Base64Data = null, FileName = null };

            // If it's a data URI: data:image/jpeg;base64,xxxxx
            var parts = rawImage.Split(",", 2);
            if (parts.Length == 2)
            {
                return new GalleryImageDto
                {
                    Url = null,
                    Base64Data = parts[1], // Extract base64 part only
                    FileName = null
                };
            }

            // Fallback for malformed data
            return new GalleryImageDto { Url = null, Base64Data = null, FileName = null };
        }
    }
}
