using KulturPlatform.Domain.Commons.Constants;
using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class Admin : AuditableEntity, IAggregateRoot
    {
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public Name Name { get; private set; }
        public string Role { get; private set; } = Roles.User; // Default role
        public bool IsActive { get; private set; } = true;
        public DateTime? LastLoginAt { get; private set; }

        private Admin(Guid id) : base(id) { }
        
        private Admin(Guid id, Email email, Password password, Name name, string role = Roles.User) : base(id)
        {
            SetEmail(email);
            SetPassword(password);
            Name = name;
            SetRole(role);
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }
        
        public static Admin CreateNew(Email email, Password password, Name name, string role = Roles.User)
        {
            return new Admin(Guid.NewGuid(), email, password, name, role);
        }
        
        public void SetEmail(Email email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }
        
        public void SetPassword(Password passwordHash)
        {
            Password = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        }
        
        public void SetName(Name name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void SetRole(string role)
        {
            if (!Roles.IsValid(role))
                throw new ArgumentException($"Invalid role: {role}. Valid roles: {string.Join(", ", Roles.All)}");
            
            Role = role;
            SetUpdatedAt();
        }

        public void UpdateEmail(Email email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            SetUpdatedAt();
        }
        
        public void UpdatePassword(Password password)
        {
            Password = password ?? throw new ArgumentNullException(nameof(password));
            SetUpdatedAt();
        }
        
        public void UpdateName(Name? name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            SetUpdatedAt();
        }
        
        public void UpdateRole(string role)
        {
            SetRole(role);
        }
        
        public void Activate()
        {
            IsActive = true;
            SetUpdatedAt();
        }

        public void Deactivate()
        {
            IsActive = false;
            SetUpdatedAt();
        }

        public void UpdateLastLogin()
        {
            LastLoginAt = DateTime.UtcNow;
            SetUpdatedAt();
        }

        // Role check methods
        public bool IsSystemAdmin() => Role == Roles.SystemAdmin;
        public bool IsUserAdmin() => Role == Roles.UserAdmin || Role == Roles.SystemAdmin;
        public bool IsUser() => Role == Roles.User || Role == Roles.UserAdmin || Role == Roles.SystemAdmin;
    }
}
