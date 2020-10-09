using System.Collections.Generic;

namespace SocialMediaDashboard.Domain.Entities
{
    /// <summary>
    /// Observation entity.
    /// </summary>
    public class Observation
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Navigation on SubscriptionTypes.
        /// </summary>
        public ICollection<SubscriptionType> SubscriptionTypes { get; set; }
    }
}
