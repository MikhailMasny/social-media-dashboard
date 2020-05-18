using SocialMediaDashboard.Common.Interfaces;

namespace SocialMediaDashboard.Domain.Models
{
    /// <summary>
    /// User profile.
    /// </summary>
    public class Profile : IHasDbIdentity, IHasUserIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string UserId { get; set; }
    }
}
