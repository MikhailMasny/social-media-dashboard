using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Entities;

namespace SocialMediaDashboard.Application.Mappings
{
    /// <summary>
    /// AutoMapper profile for Platform.
    /// </summary>
    public class PlatformProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PlatformProfile()
        {
            CreateMap<Platform, PlatformDto>().ReverseMap();
        }
    }
}
