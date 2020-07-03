namespace SocialMediaDashboard.WebAPI.Contracts.Requests
{
    /// <summary>
    /// Sentry request.
    /// </summary>
    public class SentrySettingsRequest
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
