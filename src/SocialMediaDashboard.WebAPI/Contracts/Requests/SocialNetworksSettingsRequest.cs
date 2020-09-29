using SocialMediaDashboard.Common.Helpers;

namespace SocialMediaDashboard.WebAPI.Contracts.Requests
{
    /// <summary>
    /// Vk request.
    /// </summary>
    public class SocialNetworksSettingsRequest
    {
        /// <summary>
        /// Vk access token.
        /// </summary>
        public string VkAccessToken { get; set; }

        /// <summary>
        /// Instagram account.
        /// </summary>
        public InstagramAccountSettings InstagramAccount { get; set; }

        /// <summary>
        /// YouTube access token.
        /// </summary>
        public string YouTubeAccessToken { get; set; }
    }
}
