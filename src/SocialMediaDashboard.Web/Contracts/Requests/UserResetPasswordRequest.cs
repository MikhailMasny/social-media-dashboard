namespace SocialMediaDashboard.Web.Contracts.Requests
{
    /// <summary>
    /// User reset password request.
    /// </summary>
    public class UserResetPasswordRequest
    {
        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Verify code.
        /// </summary>
        public string Code { get; set; }
    }
}
