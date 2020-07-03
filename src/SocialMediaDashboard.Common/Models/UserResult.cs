namespace SocialMediaDashboard.Common.Models
{
    /// <summary>
    /// User data transfet object.
    /// </summary>
    public class UserResult
    {
        /// <summary>
        /// GUID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Phone number.
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
