using KulturPlatform.Application.Dtos;

namespace KulturPlatform.Application.Interfaces.VolunteerSubmission
{
    public interface IVolunteerSubmissionReadService
    {
        Task<IEnumerable<VolunteerSubmissionDto>> GetAllAsync();
        Task<VolunteerSubmissionDto?> GetByIdAsync(Guid id);
    }
}
