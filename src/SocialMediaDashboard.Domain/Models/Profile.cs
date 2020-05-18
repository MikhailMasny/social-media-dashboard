using SocialMediaDashboard.Common.Interfaces;
using System;

namespace SocialMediaDashboard.Domain.Models
{
    /// <summary>
    /// User profile.
    /// </summary>
    public class Profile : IHasDbIdentity
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

        /// <summary>
        /// User identifier.
        /// </summary>
        public Guid UserId { get; set; }
    }
}
