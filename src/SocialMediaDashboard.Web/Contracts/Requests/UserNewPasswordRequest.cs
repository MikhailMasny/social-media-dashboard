namespace SocialMediaDashboard.Web.Contracts.Requests
{
    /// <summary>
    ///  User new password request.
    /// </summary>
    public class UserNewPasswordRequest
    {
        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
