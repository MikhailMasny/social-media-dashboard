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
            bool result;
            switch (accountType)
            {
                case AccountType.Vk:
                    {
                        result = false;
                    }
                    break;
                default:
                    {
                        result = true;
                    }
                    break;
            }

            return result;
        }

        /// <summary>
        /// Check subscription value.
        /// </summary>
        /// <param name="subscriptionType">Subscription type.</param>
        /// <returns>Operation result.</returns>
        public static bool CheckSubscriptionValue(this SubscriptionType subscriptionType)
        {
            bool result;
            switch (subscriptionType)
            {
                case SubscriptionType.Follower:
                case SubscriptionType.Friend:
                    {
                        result = false;
                    }
                    break;
                default:
                    {
                        result = true;
                    }
                    break;
            }

            return result;
        }
    }
}
