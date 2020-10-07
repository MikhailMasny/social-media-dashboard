using SocialMediaDashboard.Common.Enums;

namespace SocialMediaDashboard.Common.Extensions
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
        public static bool ValidateAccountType(this AccountType accountType)
        {
            return accountType switch
            {
                AccountType.Vk => false,
                AccountType.Instagram => false,
                AccountType.YouTube => false,
                _ => true
            };
        }

        /// <summary>
        /// Validate subscription value.
        /// </summary>
        /// <param name="subscriptionType">Subscription type.</param>
        /// <returns>Operation result.</returns>
        public static bool ValidateSubscriptionType(this SubscriptionType subscriptionType)
        {
            return subscriptionType switch
            {
                SubscriptionType.Follower => false,
                SubscriptionType.Friend => false,
                SubscriptionType.Subscriber => false,
                _ => true
            };
        }
    }
}
