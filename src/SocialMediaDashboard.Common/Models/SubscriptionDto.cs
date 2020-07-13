using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Interfaces;

namespace SocialMediaDashboard.Common.Models
{
    /// <summary>
    /// Statistic data transfet object.
    /// </summary>
    public class SubscriptionDto : IHasDbIdentity
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
        /// Account identifier.
        /// </summary>
        public int AccountId { get; set; }
    }
}
