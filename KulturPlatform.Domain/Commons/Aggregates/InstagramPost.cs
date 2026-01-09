using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.AggregateRoot
{
    public class InstagramPost : AuditableEntity, IAggregateRoot
    {
        public Url ImageUrl { get; private set; }
        public Url? Link { get; private set; }

        private InstagramPost(Guid id) : base(id) { }

        public static InstagramPost Create(Url imageUrl, Url? link = null)
        {
            return new InstagramPost(Guid.NewGuid())
            {
                ImageUrl = imageUrl,
                Link = link,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
