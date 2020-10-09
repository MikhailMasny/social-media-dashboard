using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SocialMediaDashboard.Domain.Entities
{
    /// <summary>
    /// User entity by IdentityUser.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Navigation on Profile.
        /// </summary>
        public Profile Profile { get; set; }

        /// <summary>
        /// Navigation on RefreshTokens.
        /// </summary>
        public ICollection<RefreshToken> RefreshTokens { get; set; }

        /// <summary>
        /// Navigation on Counters.
        /// </summary>
        public ICollection<Subscription> Counters { get; set; }
    }
}
