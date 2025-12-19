using KulturPlatform.Application.Interfaces.VolunteerSubmission;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class VolunteerSubmissionRepository : IVolunteerSubmissionRepository
    {
        private readonly AppDbContext _context;

        public VolunteerSubmissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VolunteerSubmission?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.VolunteerSubmissions.FindAsync([id], cancellationToken);
        }

        public async Task AddAsync(VolunteerSubmission submission, CancellationToken cancellationToken)
        {
            await _context.VolunteerSubmissions.AddAsync(submission, cancellationToken);
        }

        public void Update(VolunteerSubmission submission, CancellationToken cancellationToken)
        {
            _context.VolunteerSubmissions.Update(submission);
        }

        public void Delete(VolunteerSubmission submission, CancellationToken cancellationToken)
        {
            _context.VolunteerSubmissions.Remove(submission);
        }
    }
}
