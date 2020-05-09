using SocialMediaDashboard.Common.Interfaces;

namespace SocialMediaDashboard.Common.DTO
{
    /// <summary>
    /// User data transfer object.
    /// </summary>
    public class UserDTO : IHasDbIdentity
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
        /// Avatar.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Role.
        /// </summary>
        public string Role { get; set; }
    }
}
