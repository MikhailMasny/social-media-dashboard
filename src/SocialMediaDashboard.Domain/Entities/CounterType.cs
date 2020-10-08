using System.Collections.Generic;

namespace SocialMediaDashboard.Domain.Entities
{
    public class CounterType
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Account type identifier.
        /// </summary>
        public int PlatformId { get; set; }

        /// <summary>
        /// Navigation to AccountType.
        /// </summary>
        public Platform Platform { get; set; }

        /// <summary>
        /// Subscription type identifier.
        /// </summary>
        public int KindId { get; set; }

        /// <summary>
        /// Navigation to SubscriptionType.
        /// </summary>
        public Kind Kind { get; set; }

        /// <summary>
        /// Navigation to Counters.
        /// </summary>
        public ICollection<Counter> Counters { get; set; }
    }
}
