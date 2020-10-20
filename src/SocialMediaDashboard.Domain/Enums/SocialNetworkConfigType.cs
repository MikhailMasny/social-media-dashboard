namespace SocialMediaDashboard.Domain.Enums
{
    /// <summary>
    /// Application social networks configuration.
    /// </summary>
    public enum SocialNetworkConfigType
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Vk access token.
        /// </summary>
        VkAccessToken = 1,

        /// <summary>
        /// Instagram username.
        /// </summary>
        InstagramUsername = 2,

        /// <summary>
        /// Instagram password.
        /// </summary>
        InstagramPassword = 3,

        /// <summary>
        /// YouTube access token.
        /// </summary>
        YouTubeAccessToken = 4,
    }
}
