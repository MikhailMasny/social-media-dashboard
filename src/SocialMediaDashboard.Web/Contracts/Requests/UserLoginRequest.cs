namespace SocialMediaDashboard.Web.Contracts.Requests
{
    /// <summary>
    /// Login request.
    /// </summary>
    public class UserLoginRequest
    {
        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }
    }
}
