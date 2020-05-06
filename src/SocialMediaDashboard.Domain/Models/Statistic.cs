using SocialMediaDashboard.Common.Interfaces;
using System;

namespace SocialMediaDashboard.Domain.Models
{
    /// <summary>
    /// Statistic model.
    /// </summary>
    public class Statistic : IHasDbIdentity
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Count.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Date of synchronization.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Media identifier.
        /// </summary>
        public int MediaId { get; set; }

        /// <summary>
        /// Navigation property for Media.
        /// </summary>
        public Media Media { get; set; }
    }
}
