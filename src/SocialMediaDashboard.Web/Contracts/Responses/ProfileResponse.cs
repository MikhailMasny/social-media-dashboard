using SocialMediaDashboard.Application.Models;

namespace SocialMediaDashboard.Web.Contracts.Responses
{
    /// <summary>
    /// Profile response.
    /// </summary>
    public class ProfileResponse
    {
        /// <summary>
        /// Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Profile data.
        /// </summary>
        public ProfileDto Data { get; set; }
    }
}
