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
        public static bool ValidateAccountType(this AccountKind accountType)
        {
            return accountType switch
            {
                AccountKind.Vk => false,
                AccountKind.Instagram => false,
                AccountKind.YouTube => false,
                _ => true
            };
        }

        /// <summary>
        /// Validate subscription value.
        /// </summary>
        /// <param name="subscriptionType">Subscription type.</param>
        /// <returns>Operation result.</returns>
        public static bool ValidateSubscriptionType(this SubscriptionKind subscriptionType)
        {
            return subscriptionType switch
            {
                SubscriptionKind.Follower => false,
                SubscriptionKind.Friend => false,
                SubscriptionKind.Subscriber => false,
                _ => true
            };
        }
    }
}
