namespace KulturPlatform.Domain.Commons.Entities
{
    public abstract class AuditableEntity : Entity
    {
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }
        public bool IsDeleted => DeletedAt.HasValue;

        // DDD constructor
        protected AuditableEntity(Guid id) : base(id)
        {
            CreatedAt = DateTime.UtcNow;
        }

        // EF Core için parameterless constructor
        protected AuditableEntity() : base(Guid.Empty) { }

        public void SetUpdatedAt() => UpdatedAt = DateTime.UtcNow;

        public void Delete()
        {
            if (IsDeleted) return;
            DeletedAt = DateTime.UtcNow;
        }
    }


}