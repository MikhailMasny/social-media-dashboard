using SocialMediaDashboard.Common.Enums;

namespace SocialMediaDashboard.Common.Extensions
{
    /// <summary>
    /// Extensions for types.
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// Check account value.
        /// </summary>
        /// <param name="accountType">Account type.</param>
        /// <returns>Operation result.</returns>
        public static bool CheckAccountValue(this AccountType accountType)
        {
            return accountType switch
            {
                AccountType.Vk => false,
                AccountType.Instagram => false,
                _ => true
            };
        }

        /// <summary>
        /// Check subscription value.
        /// </summary>
        /// <param name="subscriptionType">Subscription type.</param>
        /// <returns>Operation result.</returns>
        public static bool CheckSubscriptionValue(this SubscriptionType subscriptionType)
        {
            return subscriptionType switch
            {
                SubscriptionType.Follower => false,
                SubscriptionType.Friend => false,
                _ => true
            };
        }
    }
}
