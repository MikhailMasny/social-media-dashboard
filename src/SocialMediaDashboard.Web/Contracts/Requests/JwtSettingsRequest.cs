namespace SocialMediaDashboard.Web.Contracts.Requests
{
    /// <summary>
    /// JSON Web Token request.
    /// </summary>
    public class JwtSettingsRequest
    {
        /// <summary>
        /// Secret.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Token lifetime.
        /// </summary>
        public string TokenLifetime { get; set; }
    }
}
