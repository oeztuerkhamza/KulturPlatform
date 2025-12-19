namespace KulturPlatform.Application.Interfaces.Localization
{
    /// <summary>
    /// Service for handling multilingual content
    /// Supports Turkish (tr), German (de), and English (en)
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Get localized text by key and language
        /// </summary>
        /// <param name="key">Resource key (e.g., "contact.form.title")</param>
        /// <param name="language">Language code: tr, de, en</param>
        /// <returns>Localized text</returns>
        string GetText(string key, string language = "tr");

        /// <summary>
        /// Get all translations for a specific section
        /// </summary>
        /// <param name="section">Section name (e.g., "contact", "volunteer")</param>
        /// <param name="language">Language code</param>
        /// <returns>Dictionary of key-value pairs</returns>
        Dictionary<string, string> GetSection(string section, string language = "tr");

        /// <summary>
        /// Check if a key exists in resources
        /// </summary>
        bool KeyExists(string key, string language = "tr");

        /// <summary>
        /// Get supported languages
        /// </summary>
        List<string> GetSupportedLanguages();
    }
}
