using System;

namespace SocialMediaDashboard.Common.Helpers
{
    /// <summary>
    /// Application settings.
    /// </summary>
    public class ApplicationSettings
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
