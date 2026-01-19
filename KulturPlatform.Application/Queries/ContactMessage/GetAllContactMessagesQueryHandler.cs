using KulturPlatform.Application.Dtos.ContactMessages;
using KulturPlatform.Application.Interfaces.ContactMessages;
using MediatR;

namespace KulturPlatform.Application.Queries.ContactMessages
{
    public class GetAllContactMessagesQueryHandler : IRequestHandler<GetAllContactMessagesQuery, IEnumerable<ContactMessageDto>>
    {
        private readonly IContactMessageReadService _readService;

        public GetAllContactMessagesQueryHandler(IContactMessageReadService readService)
        {
            _readService = readService;
        }

        public async Task<IEnumerable<ContactMessageDto>> Handle(GetAllContactMessagesQuery request, CancellationToken cancellationToken)
        {
            return await _readService.GetAllAsync(cancellationToken);
        }
    }
}
