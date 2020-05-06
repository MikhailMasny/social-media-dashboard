using SocialMediaDashboard.Common.Interfaces;
using System.Collections.Generic;

namespace SocialMediaDashboard.Domain.Models
{
    /// <summary>
    /// User model.
    /// </summary>
    public class User : IHasDbIdentity
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
        /// Password.
        /// </summary>
        public string Password { get; set; }

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
