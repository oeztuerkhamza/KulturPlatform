using KulturPlatform.Domain.Commons.Entities;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot(Guid id) : base(id)
        {
        }
    }
}
