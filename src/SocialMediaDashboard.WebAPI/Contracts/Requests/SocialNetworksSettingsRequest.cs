using SocialMediaDashboard.Common.Helpers;

namespace SocialMediaDashboard.WebAPI.Contracts.Requests
{
    /// <summary>
    /// Vk request.
    /// </summary>
    public class SocialNetworksSettingsRequest
    {
        /// <summary>
        /// Access token.
        /// </summary>
        public string VkAccessToken { get; set; }

        /// <summary>
        /// Instagram account.
        /// </summary>
        public InstagramAccountSettings InstagramAccount { get; set; }
    }
}
