namespace SocialMediaDashboard.Common.DTO
{
    /// <summary>
    /// Registration data transfet object..
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
