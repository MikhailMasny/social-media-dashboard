using SocialMediaDashboard.Common.Interfaces;

namespace SocialMediaDashboard.Common.DTO
{
    /// <summary>
    /// Token data transfer object.
    /// </summary>
    public class TokenDTO : IHasDbIdentity
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Role.
        /// </summary>
        public string Role { get; set; }
    }
}
