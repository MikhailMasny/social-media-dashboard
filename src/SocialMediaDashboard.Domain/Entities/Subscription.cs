using System.Collections.Generic;

namespace SocialMediaDashboard.Domain.Entities
{
    /// <summary>
    /// Subscription entity.
    /// </summary>
    public class Subscription
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Navigation for User.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Subscription type identifier.
        /// </summary>
        public int SubscriptionTypeId { get; set; }

        /// <summary>
        /// Navigation for SubscriptionType.
        /// </summary>
        public SubscriptionType SubscriptionType { get; set; }

        /// <summary>
        /// Navigation on Statistics.
        /// </summary>
        public ICollection<Statistic> Statistics { get; set; }
    }
}
