namespace SocialMediaDashboard.Web.Contracts.Requests
{
    /// <summary>
    /// User sign in request.
    /// </summary>
    public class UserSignInRequest
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
