using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Application.Interfaces.ContactInfo
{
    public interface IContactInfoRepository : IRepository<Domain.Commons.Aggregates.ContactInfo>
    {
    }
}
