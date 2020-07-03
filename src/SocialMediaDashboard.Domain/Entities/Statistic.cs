using SocialMediaDashboard.Common.Interfaces;
using System;

namespace SocialMediaDashboard.Domain.Entities
{
    /// <summary>
    /// Statistic entity.
    /// </summary>
    public class Statistic : IHasDbIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Count.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Date of synchronization.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Subscription identifier.
        /// </summary>
        public int SubscriptionId { get; set; }

        /// <summary>
        /// Navigation property for Subscription.
        /// </summary>
        public Subscription Subscription { get; set; }
    }
}
