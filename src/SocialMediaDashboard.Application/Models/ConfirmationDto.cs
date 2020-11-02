namespace SocialMediaDashboard.Application.Models
{
    /// <summary>
    /// Registration data transfet object.
    /// </summary>
    public class ConfirmationDto
    {
        /// <summary>
        /// Email or Name.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Verify code.
        /// </summary>
        public string Code { get; set; }
    }
}
