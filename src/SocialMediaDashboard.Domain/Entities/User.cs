using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SocialMediaDashboard.Domain.Entities
{
    /// <summary>
    /// User by IdentityUser.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Navigation to Counter.
        /// </summary>
        public ICollection<Counter> Counters { get; set; }
    }
}
