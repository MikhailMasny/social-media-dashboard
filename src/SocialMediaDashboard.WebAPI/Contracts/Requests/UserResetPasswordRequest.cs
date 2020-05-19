using System.ComponentModel.DataAnnotations;

namespace SocialMediaDashboard.WebAPI.Contracts.Requests
{
    /// <summary>
    /// User reset password request.
    /// </summary>
    public class UserResetPasswordRequest
    {
        /// <summary>
        /// Email.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Verify code.
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}
