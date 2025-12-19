using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.VolunteerSubmission;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class VolunteerSubmissionReadService : IVolunteerSubmissionReadService
    {
        private readonly AppDbContext _context;

        public VolunteerSubmissionReadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VolunteerSubmissionDto>> GetAllAsync()
        {
            return await _context.VolunteerSubmissions
                .AsNoTracking()
                .OrderByDescending(v => v.SubmittedAt)
                .Select(v => new VolunteerSubmissionDto
                {
                    Id = v.Id,
                    FullName = v.FullName.Value,
                    Email = v.Email.Value,
                    PhoneNumber = v.PhoneNumber.Value,
                    Message = v.Message.Value,
                    SubmittedAt = v.SubmittedAt
                })
                .ToListAsync();
        }

        public async Task<VolunteerSubmissionDto?> GetByIdAsync(Guid id)
        {
            return await _context.VolunteerSubmissions
                .AsNoTracking()
                .Where(v => v.Id == id)
                .Select(v => new VolunteerSubmissionDto
                {
                    Id = v.Id,
                    FullName = v.FullName.Value,
                    Email = v.Email.Value,
                    PhoneNumber = v.PhoneNumber.Value,
                    Message = v.Message.Value,
                    SubmittedAt = v.SubmittedAt
                })
                .FirstOrDefaultAsync();
        }
    }
}
