using System.Collections.Generic;

namespace SocialMediaDashboard.Domain.Entities
{
    /// <summary>
    /// Subscription type entity.
    /// </summary>
    public class SubscriptionType
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Platform identifier.
        /// </summary>
        public int PlatformId { get; set; }

        /// <summary>
        /// Navigation for Platform.
        /// </summary>
        public Platform Platform { get; set; }

        /// <summary>
        /// Observation identifier.
        /// </summary>
        public int ObservationId { get; set; }

        /// <summary>
        /// Navigation for Observation.
        /// </summary>
        public Observation Observation { get; set; }

        /// <summary>
        /// Navigation on Subscriptions.
        /// </summary>
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
