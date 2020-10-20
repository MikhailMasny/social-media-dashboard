namespace SocialMediaDashboard.Web.Contracts.Requests
{
    /// <summary>
    /// User sign up request.
    /// </summary>
    public class UserSignUpRequest
    {
        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string Name { get; set; }
    }
}
