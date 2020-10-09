using System.Collections.Generic;

namespace SocialMediaDashboard.Domain.Entities
{
    /// <summary>
    /// Platform entity.
    /// </summary>
    public class Platform
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
