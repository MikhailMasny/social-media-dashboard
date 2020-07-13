namespace SocialMediaDashboard.Common.Enums
{
    /// <summary>
    /// Application Sentry configuration.
    /// </summary>
    public enum SentryConfigType
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Domain Name System.
        /// </summary>
        Dns = 1,

        /// <summary>
        /// Minimum breadcrumb level.
        /// </summary>
        MinimumBreadcrumbLevel = 2,

        /// <summary>
        /// Minimum event level.
        /// </summary>
        MinimumEventLevel = 3
    }
}
