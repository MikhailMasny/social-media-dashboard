namespace SocialMediaDashboard.WebAPI.Contracts.Requests
{
    /// <summary>
    /// Registration request.
    /// </summary>
    public class UserRegistrationRequest
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
