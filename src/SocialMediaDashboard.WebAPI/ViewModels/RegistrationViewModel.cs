using System.ComponentModel.DataAnnotations;

namespace SocialMediaDashboard.WebAPI.ViewModels
{
    /// <summary>
    /// Registration ViewModel.
    /// </summary>
    public class RegistrationViewModel
    {
        /// <summary>
        /// Email.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Password confirm.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
    }
}
