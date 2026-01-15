using KulturPlatform.Application.Dtos;
using System.Threading;

namespace KulturPlatform.Application.Interfaces.DonatePage
{
    public interface IDonatePageReadService
    {
        Task<DonatePageDto?> GetDonatePageAsync(CancellationToken cancellationToken = default);
    }
}
