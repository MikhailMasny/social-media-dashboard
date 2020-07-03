using SocialMediaDashboard.Common.Models;
using SocialMediaDashboard.Domain.Entities;

namespace SocialMediaDashboard.Data.Mappings
{
    /// <summary>
    /// Профиль AutoMapper для Media.
    /// </summary>
    public class MediaProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public MediaProfile()
        {
            CreateMap<Media, MediaDto>().ReverseMap();
        }
    }
}
