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
        /// User nickname.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }
    }
}
