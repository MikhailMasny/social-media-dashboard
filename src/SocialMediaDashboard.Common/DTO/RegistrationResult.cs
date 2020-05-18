namespace SocialMediaDashboard.Common.DTO
{
    /// <summary>
    /// Email confirmation result.
    /// </summary>
    public class RegistrationResult : ResultBase
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Verify code.
        /// </summary>
        public string Code { get; set; }
    }
}
