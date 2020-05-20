namespace SocialMediaDashboard.WebAPI.Contracts.Requests
{
    /// <summary>
    /// User restore password request.
    /// </summary>
    public class UserRestorePasswordRequest
    {
        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }
    }
}
