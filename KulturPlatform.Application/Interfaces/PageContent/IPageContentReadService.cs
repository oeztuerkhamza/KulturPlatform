using KulturPlatform.Application.Dtos;

namespace KulturPlatform.Application.Interfaces.PageContent
{
    public interface IPageContentReadService
    {
        Task<IEnumerable<PageContentDto>> GetAllAsync();
        Task<PageContentDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<PageContentDto>> GetByPageNameAsync(string pageName);
    }
}
