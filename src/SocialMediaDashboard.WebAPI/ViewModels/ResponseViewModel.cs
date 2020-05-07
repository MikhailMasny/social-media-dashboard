using SocialMediaDashboard.Common.DTO;

namespace SocialMediaDashboard.WebAPI.ViewModels
{
    /// <summary>
    /// Authentication response ViewModel.
    /// </summary>
    public class ResponseViewModel
    {
        /// <summary>
        /// Result of response.
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// User data.
        /// </summary>
        public UserDTO User { get; set; }
    }
}
