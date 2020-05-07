using System.ComponentModel.DataAnnotations;

namespace SocialMediaDashboard.WebAPI.ViewModels
{
    /// <summary>
    /// Authenticate ViewModel.
    /// </summary>
    public class LoginViewModel
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
        public string Password { get; set; }
    }
}
