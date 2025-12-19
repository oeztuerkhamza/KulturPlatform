using KulturPlatform.Application.Interfaces.Localization;
using KulturPlatform.Application.Interfaces.LocalizationResource;
using Microsoft.Extensions.Caching.Memory;

namespace KulturPlatform.Infrastructure.Services
{
    /// <summary>
    /// Database-backed localization service with in-memory caching
    /// Fallback to hardcoded translations if database is empty
    /// </summary>
    public class LocalizationService : ILocalizationService
    {
        private readonly ILocalizationResourceReadService _readService;
        private readonly IMemoryCache _cache;
        private readonly Dictionary<string, Dictionary<string, string>> _fallbackResources;
        private readonly List<string> _supportedLanguages = new() { "tr", "de", "en" };
        private const string CacheKeyPrefix = "localization_";
        private const int CacheExpirationMinutes = 30;

        public LocalizationService(
            ILocalizationResourceReadService readService,
            IMemoryCache cache)
        {
            _readService = readService;
            _cache = cache;
            _fallbackResources = InitializeFallbackResources();
        }

        public string GetText(string key, string language = "tr")
        {
            language = NormalizeLanguage(language);
            var cacheKey = $"{CacheKeyPrefix}{language}";

            // Try to get from cache
            if (!_cache.TryGetValue(cacheKey, out Dictionary<string, string>? translations))
            {
                // Load from database
                translations = _readService.GetTranslationsAsync(language).Result;
                
                // If database is empty, use fallback
                if (translations == null || !translations.Any())
                {
                    translations = _fallbackResources.ContainsKey(language)
                        ? _fallbackResources[language]
                        : _fallbackResources["tr"];
                }

                // Cache it
                _cache.Set(cacheKey, translations, TimeSpan.FromMinutes(CacheExpirationMinutes));
            }

            if (translations!.TryGetValue(key, out var text))
            {
                return text;
            }

            // Fallback to Turkish if not found
            if (language != "tr" && _cache.TryGetValue($"{CacheKeyPrefix}tr", out Dictionary<string, string>? trTranslations))
            {
                if (trTranslations!.TryGetValue(key, out var trText))
                {
                    return trText;
                }
            }

            return key; // Return key itself if not found
        }

        public Dictionary<string, string> GetSection(string section, string language = "tr")
        {
            language = NormalizeLanguage(language);
            var sectionCacheKey = $"{CacheKeyPrefix}{section}_{language}";

            if (!_cache.TryGetValue(sectionCacheKey, out Dictionary<string, string>? sectionTranslations))
            {
                sectionTranslations = _readService.GetSectionTranslationsAsync(section, language).Result;

                if (sectionTranslations == null || !sectionTranslations.Any())
                {
                    // Fallback
                    var sectionPrefix = $"{section}.";
                    if (_fallbackResources.ContainsKey(language))
                    {
                        sectionTranslations = _fallbackResources[language]
                            .Where(kv => kv.Key.StartsWith(sectionPrefix))
                            .ToDictionary(kv => kv.Key, kv => kv.Value);
                    }
                    else
                    {
                        sectionTranslations = new Dictionary<string, string>();
                    }
                }

                _cache.Set(sectionCacheKey, sectionTranslations, TimeSpan.FromMinutes(CacheExpirationMinutes));
            }

            return sectionTranslations ?? new Dictionary<string, string>();
        }

        public bool KeyExists(string key, string language = "tr")
        {
            var text = GetText(key, language);
            return text != key; // If it returns the key itself, it doesn't exist
        }

        public List<string> GetSupportedLanguages()
        {
            return _supportedLanguages;
        }

        /// <summary>
        /// Clear cache (call this when translations are updated in admin panel)
        /// </summary>
        public void ClearCache()
        {
            foreach (var lang in _supportedLanguages)
            {
                _cache.Remove($"{CacheKeyPrefix}{lang}");
            }
        }

        private string NormalizeLanguage(string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                return "tr";

            language = language.ToLowerInvariant().Trim();
            return _supportedLanguages.Contains(language) ? language : "tr";
        }

        // Fallback translations (same as before)
        private Dictionary<string, Dictionary<string, string>> InitializeFallbackResources()
        {
            return new Dictionary<string, Dictionary<string, string>>
            {
                ["tr"] = InitializeTurkishResources(),
                ["de"] = InitializeGermanResources(),
                ["en"] = InitializeEnglishResources()
            };
        }

        private Dictionary<string, string> InitializeTurkishResources()
        {
            return new Dictionary<string, string>
            {
                // Contact Form
                ["contact.form.title"] = "Bize Ula??n",
                ["contact.form.description"] = "Sorular?n?z için bizimle ileti?ime geçebilirsiniz. En k?sa sürede size geri dönü? yapaca??z.",
                ["contact.form.name.label"] = "Ad Soyad",
                ["contact.form.name.placeholder"] = "Ad?n?z? ve soyad?n?z? giriniz",
                ["contact.form.email.label"] = "E-posta",
                ["contact.form.email.placeholder"] = "ornek@email.com",
                ["contact.form.phone.label"] = "Telefon",
                ["contact.form.phone.placeholder"] = "+49 123 456 7890",
                ["contact.form.subject.label"] = "Konu",
                ["contact.form.subject.placeholder"] = "Mesaj?n?z?n konusu",
                ["contact.form.message.label"] = "Mesaj",
                ["contact.form.message.placeholder"] = "Mesaj?n?z? buraya yaz?n?z...",
                ["contact.form.submit"] = "Gönder",
                ["contact.form.success"] = "? Mesaj?n?z ba?ar?yla gönderildi! En k?sa sürede size geri dönü? yapaca??z.",
                ["contact.form.error"] = "? Mesaj gönderilirken bir hata olu?tu. Lütfen tekrar deneyiniz.",
                
                // Contact Validation
                ["contact.validation.name.required"] = "Ad Soyad zorunludur",
                ["contact.validation.email.required"] = "E-posta zorunludur",
                ["contact.validation.email.invalid"] = "Geçerli bir e-posta adresi giriniz",
                ["contact.validation.subject.required"] = "Konu zorunludur",
                ["contact.validation.message.required"] = "Mesaj zorunludur",
                
                ["contact.map.title"] = "Ofis Konumumuz",
                
                // Volunteer
                ["volunteer.hero.title"] = "Gönüllü Ol",
                ["volunteer.hero.subtitle"] = "Kultur Platform Frankfurt'un Bir Parças? Olun",
                ["volunteer.hero.description"] = "Frankfurt'ta ya?ayan Türk toplumuna hizmet etmek, kültürel etkinlikler düzenlemek ve toplumsal projelerde yer almak için gönüllü olun.",
                ["volunteer.hero.cta"] = "Hemen Ba?vur",
                
                ["volunteer.form.title"] = "Gönüllü Ba?vuru Formu",
                ["volunteer.form.description"] = "A?a??daki formu doldurarak gönüllü ba?vurunuzu yapabilirsiniz. En k?sa sürede size geri dönü? yapaca??z.",
                ["volunteer.form.name.label"] = "Ad Soyad",
                ["volunteer.form.email.label"] = "E-posta",
                ["volunteer.form.phone.label"] = "Telefon",
                ["volunteer.form.message.label"] = "Neden Gönüllü Olmak ?stiyorsunuz?",
                ["volunteer.form.submit"] = "Ba?vuruyu Gönder",
                ["volunteer.form.success"] = "? Ba?vurunuz ba?ar?yla al?nd?! En k?sa sürede size geri dönü? yapaca??z.",
                ["volunteer.form.error"] = "? Ba?vuru gönderilirken bir hata olu?tu. Lütfen tekrar deneyiniz.",
                
                ["volunteer.benefit.community.title"] = "Topluma Katk?",
                ["volunteer.benefit.community.description"] = "Frankfurt'taki Türk toplulu?una anlaml? katk?lar sa?lay?n",
                ["volunteer.benefit.skills.title"] = "Yeni Beceriler",
                ["volunteer.benefit.skills.description"] = "Etkinlik organizasyonu, sosyal medya yönetimi gibi yeni beceriler kazan?n",
                ["volunteer.benefit.network.title"] = "Networking",
                ["volunteer.benefit.network.description"] = "Ayn? de?erleri payla?an insanlarla tan???n ve network'ünüzü geni?letin",
                ["volunteer.benefit.certificate.title"] = "Sertifika",
                ["volunteer.benefit.certificate.description"] = "Gönüllülük çal??malar?n?z için resmi sertifika al?n"
            };
        }

        private Dictionary<string, string> InitializeGermanResources()
        {
            return new Dictionary<string, string>
            {
                // Contact Form
                ["contact.form.title"] = "Kontaktieren Sie uns",
                ["contact.form.description"] = "Bei Fragen können Sie uns gerne kontaktieren. Wir melden uns schnellstmöglich bei Ihnen zurück.",
                ["contact.form.name.label"] = "Name",
                ["contact.form.name.placeholder"] = "Geben Sie Ihren Namen ein",
                ["contact.form.email.label"] = "E-Mail",
                ["contact.form.email.placeholder"] = "beispiel@email.com",
                ["contact.form.phone.label"] = "Telefon",
                ["contact.form.phone.placeholder"] = "+49 123 456 7890",
                ["contact.form.subject.label"] = "Betreff",
                ["contact.form.subject.placeholder"] = "Betreff Ihrer Nachricht",
                ["contact.form.message.label"] = "Nachricht",
                ["contact.form.message.placeholder"] = "Schreiben Sie hier Ihre Nachricht...",
                ["contact.form.submit"] = "Senden",
                ["contact.form.success"] = "? Ihre Nachricht wurde erfolgreich gesendet! Wir melden uns schnellstmöglich bei Ihnen.",
                ["contact.form.error"] = "? Beim Senden der Nachricht ist ein Fehler aufgetreten. Bitte versuchen Sie es erneut.",
                
                // Contact Validation
                ["contact.validation.name.required"] = "Name ist erforderlich",
                ["contact.validation.email.required"] = "E-Mail ist erforderlich",
                ["contact.validation.email.invalid"] = "Geben Sie eine gültige E-Mail-Adresse ein",
                ["contact.validation.subject.required"] = "Betreff ist erforderlich",
                ["contact.validation.message.required"] = "Nachricht ist erforderlich",
                
                ["contact.map.title"] = "Unser Standort",
                
                // Volunteer
                ["volunteer.hero.title"] = "Werden Sie Freiwilliger",
                ["volunteer.hero.subtitle"] = "Werden Sie Teil der Kultur Platform Frankfurt",
                ["volunteer.hero.description"] = "Werden Sie Freiwilliger, um der türkischen Gemeinschaft in Frankfurt zu dienen, kulturelle Veranstaltungen zu organisieren und an Gemeinschaftsprojekten teilzunehmen.",
                ["volunteer.hero.cta"] = "Jetzt bewerben",
                
                ["volunteer.form.title"] = "Freiwilligen-Bewerbungsformular",
                ["volunteer.form.description"] = "Füllen Sie das folgende Formular aus, um Ihre Freiwilligenbewerbung einzureichen. Wir werden uns schnellstmöglich bei Ihnen melden.",
                ["volunteer.form.name.label"] = "Name",
                ["volunteer.form.email.label"] = "E-Mail",
                ["volunteer.form.phone.label"] = "Telefon",
                ["volunteer.form.message.label"] = "Warum möchten Sie Freiwilliger werden?",
                ["volunteer.form.submit"] = "Bewerbung senden",
                ["volunteer.form.success"] = "? Ihre Bewerbung wurde erfolgreich eingereicht! Wir werden uns schnellstmöglich bei Ihnen melden.",
                ["volunteer.form.error"] = "? Beim Senden der Bewerbung ist ein Fehler aufgetreten. Bitte versuchen Sie es erneut.",
                
                ["volunteer.benefit.community.title"] = "Beitrag zur Gemeinschaft",
                ["volunteer.benefit.community.description"] = "Leisten Sie bedeutende Beiträge zur türkischen Gemeinschaft in Frankfurt",
                ["volunteer.benefit.skills.title"] = "Neue Fähigkeiten",
                ["volunteer.benefit.skills.description"] = "Erwerben Sie neue Fähigkeiten wie Eventmanagement und Social-Media-Verwaltung",
                ["volunteer.benefit.network.title"] = "Networking",
                ["volunteer.benefit.network.description"] = "Treffen Sie Menschen mit gleichen Werten und erweitern Sie Ihr Netzwerk",
                ["volunteer.benefit.certificate.title"] = "Zertifikat",
                ["volunteer.benefit.certificate.description"] = "Erhalten Sie ein offizielles Zertifikat für Ihre Freiwilligenarbeit"
            };
        }

        private Dictionary<string, string> InitializeEnglishResources()
        {
            return new Dictionary<string, string>
            {
                // Contact Form
                ["contact.form.title"] = "Contact Us",
                ["contact.form.description"] = "Feel free to contact us with your questions. We will get back to you as soon as possible.",
                ["contact.form.name.label"] = "Name",
                ["contact.form.name.placeholder"] = "Enter your name",
                ["contact.form.email.label"] = "Email",
                ["contact.form.email.placeholder"] = "example@email.com",
                ["contact.form.phone.label"] = "Phone",
                ["contact.form.phone.placeholder"] = "+49 123 456 7890",
                ["contact.form.subject.label"] = "Subject",
                ["contact.form.subject.placeholder"] = "Subject of your message",
                ["contact.form.message.label"] = "Message",
                ["contact.form.message.placeholder"] = "Write your message here...",
                ["contact.form.submit"] = "Send",
                ["contact.form.success"] = "? Your message has been sent successfully! We will get back to you as soon as possible.",
                ["contact.form.error"] = "? An error occurred while sending the message. Please try again.",
                
                // Contact Validation
                ["contact.validation.name.required"] = "Name is required",
                ["contact.validation.email.required"] = "Email is required",
                ["contact.validation.email.invalid"] = "Enter a valid email address",
                ["contact.validation.subject.required"] = "Subject is required",
                ["contact.validation.message.required"] = "Message is required",
                
                ["contact.map.title"] = "Our Address",
                
                // Volunteer
                ["volunteer.hero.title"] = "Become a Volunteer",
                ["volunteer.hero.subtitle"] = "Become a Part of Kultur Platform Frankfurt",
                ["volunteer.hero.description"] = "Volunteer to serve the Turkish community in Frankfurt, organize cultural events, and participate in community projects.",
                ["volunteer.hero.cta"] = "Apply Now",
                
                ["volunteer.form.title"] = "Volunteer Application Form",
                ["volunteer.form.description"] = "Fill out the form below to submit your volunteer application. We will get back to you as soon as possible.",
                ["volunteer.form.name.label"] = "Name",
                ["volunteer.form.email.label"] = "Email",
                ["volunteer.form.phone.label"] = "Phone",
                ["volunteer.form.message.label"] = "Why do you want to be a volunteer?",
                ["volunteer.form.submit"] = "Submit Application",
                ["volunteer.form.success"] = "? Your application has been successfully received! We will get back to you as soon as possible.",
                ["volunteer.form.error"] = "? An error occurred while submitting the application. Please try again.",
                
                ["volunteer.benefit.community.title"] = "Community Contribution",
                ["volunteer.benefit.community.description"] = "Make meaningful contributions to the Turkish community in Frankfurt",
                ["volunteer.benefit.skills.title"] = "New Skills",
                ["volunteer.benefit.skills.description"] = "Gain new skills such as event management and social media management",
                ["volunteer.benefit.network.title"] = "Networking",
                ["volunteer.benefit.network.description"] = "Meet people who share the same values and expand your network",
                ["volunteer.benefit.certificate.title"] = "Certificate",
                ["volunteer.benefit.certificate.description"] = "Receive an official certificate for your volunteer work"
            };
        }
    }
}
