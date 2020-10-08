namespace SocialMediaDashboard.Domain.Helpers
{
    /// <summary>
    /// Social settings (from appsettings.json).
    /// </summary>
    public class SocialNetworksSettings
    {
        /// <summary>
        /// Vk access token.
        /// </summary>
        public string VkAccessToken { get; set; }

        /// <summary>
        /// Instagram account settings.
        /// </summary>
        public InstagramAccountSettings InstagramAccount { get; set; }

        /// <summary>
        /// YouTube access token.
        /// </summary>
        public string YouTubeAccessToken { get; set; }
    }
}
