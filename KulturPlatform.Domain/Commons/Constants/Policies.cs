namespace KulturPlatform.Domain.Commons.Constants
{
    /// <summary>
    /// Authorization policies used in the API
    /// </summary>
    public static class Policies
    {
        /// <summary>
        /// Requires SystemAdmin role
        /// </summary>
        public const string RequireSystemAdmin = "RequireSystemAdmin";

        /// <summary>
        /// Requires UserAdmin or SystemAdmin role
        /// </summary>
        public const string RequireUserAdmin = "RequireUserAdmin";

        /// <summary>
        /// Requires User, UserAdmin, or SystemAdmin role
        /// </summary>
        public const string RequireUser = "RequireUser";
    }
}
