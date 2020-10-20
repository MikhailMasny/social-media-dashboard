using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Entities;

namespace SocialMediaDashboard.Application.Mappings
{
    /// <summary>
    /// AutoMapper profile for Subscription.
    /// </summary>
    public class SubscriptionProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SubscriptionProfile()
        {
            CreateMap<Subscription, SubscriptionDto>().ReverseMap();
        }
    }
}
