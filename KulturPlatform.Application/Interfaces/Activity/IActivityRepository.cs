using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Application.Interfaces.Activity
{
    public interface IActivityRepository : IRepository<Domain.Commons.AggregateRoot.Activity>
    {
    }
}
