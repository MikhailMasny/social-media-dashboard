using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaDashboard.Domain.Entities
{
    /// <summary>
    /// User profile entity.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Identifier.
        /// </summary>
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
        public string UserId { get; set; }

        // UNDONE: maybe fix it?
        /// <summary>
        /// User.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}
