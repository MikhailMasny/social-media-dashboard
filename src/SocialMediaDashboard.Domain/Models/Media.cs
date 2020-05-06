using SocialMediaDashboard.Common.Interfaces;
using System.Collections.Generic;

namespace SocialMediaDashboard.Domain.Models
{
    /// <summary>
    /// Media model.
    /// </summary>
    public class Media : IHasDbIdentity
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Social media account.
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// User identifier.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Navigation property for User.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Navigation to Statistic.
        /// </summary>
        public ICollection<Statistic> Statistics { get; set; }
    }
}
