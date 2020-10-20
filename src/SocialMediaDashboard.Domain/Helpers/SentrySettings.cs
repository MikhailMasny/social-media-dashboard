namespace SocialMediaDashboard.Domain.Helpers
{
    /// <summary>
    /// Sentry settings (from appsettings.json).
    /// </summary>
    public class SentrySettings
    {
        /// <summary>
        /// Domain Name System.
        /// </summary>
        public string Dsn { get; set; }

        /// <summary>
        /// Minimum breadcrumb level.
        /// </summary>
        public string MinimumBreadcrumbLevel { get; set; }

        /// <summary>
        /// Minimum event level.
        /// </summary>
        public string MinimumEventLevel { get; set; }
    }
}
