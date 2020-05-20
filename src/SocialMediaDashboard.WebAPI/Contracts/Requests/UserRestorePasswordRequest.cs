using System.ComponentModel.DataAnnotations;

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
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
