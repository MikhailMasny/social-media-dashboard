using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Interfaces;
using System.Collections.Generic;

namespace SocialMediaDashboard.Domain.Entities
{
    /// <summary>
    /// Subscription entity.
    /// </summary>
    public class Subscription : IHasDbIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        public SubscriptionType Type { get; set; }

        /// <summary>
        /// Display for user.
        /// </summary>
        public bool IsDisplayed { get; set; }

        /// <summary>
        /// Media identifier.
        /// </summary>
        public int MediaId { get; set; }

        /// <summary>
        /// Navigation property for Media.
        /// </summary>
        public Media Media { get; set; }

        /// <summary>
        /// Navigation to Statistic.
        /// </summary>
        public ICollection<Statistic> Statistics { get; }
    }
}
