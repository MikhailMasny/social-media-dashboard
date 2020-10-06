using SocialMediaDashboard.Common.Constants;

namespace SocialMediaDashboard.Common.Extensions
{
    /// <summary>
    /// Extensions for roles.
    /// </summary>
    public static class RoleExtension
    {
        /// <summary>
        /// Check role for admin.
        /// </summary>
        /// <param name="role">User role.</param>
        /// <returns>Operation result.</returns>
        public static bool IsAdmin(string role)
        {
            return role switch
            {
                AppRoles.Admin => true,
                _ => false
            };
        }
    }
}
