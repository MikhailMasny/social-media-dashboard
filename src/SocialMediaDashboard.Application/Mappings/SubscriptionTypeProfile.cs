using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Entities;

namespace SocialMediaDashboard.Application.Mappings
{
    /// <summary>
    /// AutoMapper profile for SubscriptionType.
    /// </summary>
    public class SubscriptionTypeProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SubscriptionTypeProfile()
        {
            CreateMap<SubscriptionType, SubscriptionTypeDto>().ReverseMap();
        }
    }
}
