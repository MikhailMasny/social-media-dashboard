using System;

namespace SocialMediaDashboard.Domain.Entities
{
    /// <summary>
    /// Statistic entity.
    /// </summary>
    public class Statistic
    {
        /// <summary>
        /// Identifier.
        /// </summary>
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
        /// Counter identifier.
        /// </summary>
        public int CounterId { get; set; }

        /// <summary>
        /// Navigation property for Counter.
        /// </summary>
        public Counter Counter { get; set; }
    }
}
