using AutoMapper;
using KulturPlatform.Application.Dtos.ContactMessages;
using KulturPlatform.Application.Interfaces.ContactMessages;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class ContactMessageReadService : IContactMessageReadService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContactMessageReadService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactMessageDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var messages = await _context.ContactMessages
                .OrderByDescending(m => m.SubmittedAt)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ContactMessageDto>>(messages);
        }

        public async Task<ContactMessageDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var message = await _context.ContactMessages
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

            return message != null ? _mapper.Map<ContactMessageDto>(message) : null;
        }

        public async Task<IEnumerable<ContactMessageDto>> GetUnreadAsync(CancellationToken cancellationToken)
        {
            var messages = await _context.ContactMessages
                .Where(m => !m.IsRead)
                .OrderByDescending(m => m.SubmittedAt)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ContactMessageDto>>(messages);
        }

        public async Task<int> GetUnreadCountAsync(CancellationToken cancellationToken)
        {
            return await _context.ContactMessages
                .CountAsync(m => !m.IsRead, cancellationToken);
        }
    }
}
