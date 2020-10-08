using SocialMediaDashboard.Domain.Enums;

namespace SocialMediaDashboard.Application.Models
{
    /// <summary>
    /// Statistic data transfet object.
    /// </summary>
    public class SubscriptionDto
    {
        /// <summary>
        /// Identifier.
        /// </summary>
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
