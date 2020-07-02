namespace SocialMediaDashboard.Common.DTO
{
    /// <summary>
    /// Registration data transfet object..
    /// </summary>
    public class ConfirmationResult : BaseResult
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
