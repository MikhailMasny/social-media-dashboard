using System;

namespace SocialMediaDashboard.Domain.Helpers
{
    /// <summary>
    /// Jwt settings (from appsettings.json).
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Secret JWT key.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Token Lifetime.
        /// </summary>
        public TimeSpan TokenLifetime { get; set; }
    }
}
