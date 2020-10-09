using SocialMediaDashboard.Domain.Enums;

namespace SocialMediaDashboard.Domain.Extensions
{
    /// <summary>
    /// Extensions for types.
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// Validate account value.
        /// </summary>
        /// <param name="accountType">Account type.</param>
        /// <returns>Operation result.</returns>
        public static bool ValidateAccountType(this PlatformType accountType)
        {
            return accountType switch
            {
                PlatformType.Vk => false,
                PlatformType.Instagram => false,
                PlatformType.YouTube => false,
                _ => true
            };
        }

        /// <summary>
        /// Validate subscription value.
        /// </summary>
        /// <param name="subscriptionType">Subscription type.</param>
        /// <returns>Operation result.</returns>
        public static bool ValidateSubscriptionType(this ObservationType subscriptionType)
        {
            return subscriptionType switch
            {
                ObservationType.Follower => false,
                ObservationType.Friend => false,
                ObservationType.Subscriber => false,
                _ => true
            };
        }
    }
}
