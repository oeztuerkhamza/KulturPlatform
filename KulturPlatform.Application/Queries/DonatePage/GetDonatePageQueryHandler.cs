using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.DonatePage;
using MediatR;

namespace KulturPlatform.Application.Queries.DonatePage
{
    public class GetDonatePageQueryHandler : IRequestHandler<GetDonatePageQuery, DonatePageDto?>
    {
        private readonly IDonatePageReadService _readService;

        public GetDonatePageQueryHandler(IDonatePageReadService readService)
        {
            _readService = readService;
        }

        public async Task<DonatePageDto?> Handle(GetDonatePageQuery request, CancellationToken cancellationToken)
        {
            return await _readService.GetDonatePageAsync(cancellationToken);
        }
    }
}
