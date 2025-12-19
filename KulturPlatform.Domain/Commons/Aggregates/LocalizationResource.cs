using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    /// <summary>
    /// Localization resource for multilingual content management
    /// Admin panel'den düzenlenebilir çeviriler
    /// </summary>
    public class LocalizationResource : IAggregateRoot
    {
        public Guid Id { get; private set; }
        
        /// <summary>
        /// Resource key (e.g., "contact.form.title")
        /// </summary>
        public string Key { get; private set; }
        
        /// <summary>
        /// Turkish translation
        /// </summary>
        public string Turkish { get; private set; }
        
        /// <summary>
        /// German translation
        /// </summary>
        public string German { get; private set; }
        
        /// <summary>
        /// English translation
        /// </summary>
        public string English { get; private set; }
        
        /// <summary>
        /// Section/Category (e.g., "contact", "volunteer", "common")
        /// </summary>
        public string Section { get; private set; }
        
        /// <summary>
        /// Description/Note for translators
        /// </summary>
        public string? Description { get; private set; }
        
        /// <summary>
        /// Is this resource active?
        /// </summary>
        public bool IsActive { get; private set; }
        
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public string? CreatedBy { get; private set; }
        public string? UpdatedBy { get; private set; }

        // EF Core constructor
        private LocalizationResource() { }

        /// <summary>
        /// Create new localization resource
        /// </summary>
        public static LocalizationResource CreateNew(
            string key,
            string turkish,
            string german,
            string english,
            string section,
            string? description = null,
            string? createdBy = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Resource key cannot be empty", nameof(key));
            
            if (string.IsNullOrWhiteSpace(turkish))
                throw new ArgumentException("Turkish translation is required", nameof(turkish));

            return new LocalizationResource
            {
                Id = Guid.NewGuid(),
                Key = key.Trim().ToLowerInvariant(),
                Turkish = turkish,
                German = german ?? turkish,
                English = english ?? turkish,
                Section = section?.Trim().ToLowerInvariant() ?? "common",
                Description = description,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };
        }

        /// <summary>
        /// Update translations
        /// </summary>
        public void UpdateTranslations(
            string turkish,
            string german,
            string english,
            string? description = null,
            string? updatedBy = null)
        {
            if (string.IsNullOrWhiteSpace(turkish))
                throw new ArgumentException("Turkish translation is required", nameof(turkish));

            Turkish = turkish;
            German = german ?? turkish;
            English = english ?? turkish;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }

        /// <summary>
        /// Activate resource
        /// </summary>
        public void Activate(string? updatedBy = null)
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }

        /// <summary>
        /// Deactivate resource
        /// </summary>
        public void Deactivate(string? updatedBy = null)
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }

        /// <summary>
        /// Get translation by language code
        /// </summary>
        public string GetTranslation(string languageCode)
        {
            return languageCode?.ToLowerInvariant() switch
            {
                "de" => German,
                "en" => English,
                _ => Turkish
            };
        }
    }
}
