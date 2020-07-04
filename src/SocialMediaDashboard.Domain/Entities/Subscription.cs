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
        /// Social media account identifier.
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Navigation property for Social media account.
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Navigation to Statistic.
        /// </summary>
        public ICollection<Statistic> Statistics { get; }
    }
}
