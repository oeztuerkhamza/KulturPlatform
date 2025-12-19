using KulturPlatform.Application.Dtos.SatzungDto;

namespace KulturPlatform.Application.Dtos
{
    public record PurposeDto
    {
        public string Letter { get; set; }
        public SectionContentDto Content { get; set; }
    }
}
