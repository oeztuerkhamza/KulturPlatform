namespace KulturPlatform.Domain.Commons.Constants
{
    /// <summary>
    /// Role names used throughout the application
    /// </summary>
    public static class Roles
    {
        /// <summary>
        /// System Administrator - Full system access
        /// </summary>
        public const string SystemAdmin = "SystemAdmin";

        /// <summary>
        /// User Administrator - Can manage content and users
        /// </summary>
        public const string UserAdmin = "UserAdmin";

        /// <summary>
        /// Regular User - Basic access to public features
        /// </summary>
        public const string User = "User";

        /// <summary>
        /// Get all available roles
        /// </summary>
        public static readonly string[] All = { SystemAdmin, UserAdmin, User };

        /// <summary>
        /// Check if a role is valid
        /// </summary>
        public static bool IsValid(string role) => All.Contains(role);
    }
}
