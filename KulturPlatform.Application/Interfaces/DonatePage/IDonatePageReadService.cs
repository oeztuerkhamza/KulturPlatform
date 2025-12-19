using KulturPlatform.Application.Dtos;

namespace KulturPlatform.Application.Interfaces.DonatePage
{
    public interface IDonatePageReadService
    {
        Task<DonatePageDto?> GetAsync();
    }

}
