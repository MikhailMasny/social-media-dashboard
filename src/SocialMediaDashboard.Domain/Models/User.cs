using SocialMediaDashboard.Common.Interfaces;
using System.Collections.Generic;

namespace SocialMediaDashboard.Domain.Models
{
    /// <summary>
    /// User model.
    /// </summary>
    public class User : IHasDbIdentity
    {
        public static IEnumerable<object> Claims { get; set; }

        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }

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
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Navigation to Media.
        /// </summary>
        public ICollection<Media> Medias { get; set; }
    }
}
