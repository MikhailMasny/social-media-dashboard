using System.ComponentModel.DataAnnotations;

namespace SocialMediaDashboard.WebAPI.ViewModels
{
    /// <summary>
    /// Profile ViewModel.
    /// </summary>
    public class ProfileViewModel
    {
        /// <summary>
        /// Email.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }
    }
}
