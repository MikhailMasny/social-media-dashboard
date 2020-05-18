using SocialMediaDashboard.Common.Interfaces;
using System.Collections.Generic;

namespace SocialMediaDashboard.Domain.Models
{
    /// <summary>
    /// Media model.
    /// </summary>
    public class Media : IHasDbIdentity, IHasUserIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Social media account.
        /// </summary>
        public string AccountName { get; set; }

        /// <inheritdoc/>
        public string UserId { get; set; }

        /// <summary>
        /// Navigation to Statistic.
        /// </summary>
        public ICollection<Statistic> Statistics { get; set; }
    }
}
