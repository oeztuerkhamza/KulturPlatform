using KulturPlatform.Application.Dtos.ContactMessages;
using KulturPlatform.Application.Interfaces.ContactMessages;
using MediatR;

namespace KulturPlatform.Application.Queries.ContactMessages
{
    public class GetContactMessageByIdQueryHandler : IRequestHandler<GetContactMessageByIdQuery, ContactMessageDto?>
    {
        private readonly IContactMessageReadService _readService;

        public GetContactMessageByIdQueryHandler(IContactMessageReadService readService)
        {
            _readService = readService;
        }

        public async Task<ContactMessageDto?> Handle(GetContactMessageByIdQuery request, CancellationToken cancellationToken)
        {
            return await _readService.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
