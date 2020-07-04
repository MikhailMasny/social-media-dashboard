﻿using SocialMediaDashboard.Common.Models;
using SocialMediaDashboard.Domain.Entities;

namespace SocialMediaDashboard.Data.Mappings
{
    /// <summary>
    /// Профиль AutoMapper для Subscription.
    /// </summary>
    public class SubscriptionProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public SubscriptionProfile()
        {
            CreateMap<Subscription, SubscriptionDto>().ReverseMap();
        }
    }
}