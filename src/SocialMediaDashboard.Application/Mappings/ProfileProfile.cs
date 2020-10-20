using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Entities;

namespace SocialMediaDashboard.Application.Mappings
{
    /// <summary>
    /// AutoMapper profile for Profile.
    /// </summary>
    public class ProfileProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ProfileProfile()
        {
            CreateMap<Profile, ProfileDto>().ReverseMap();
        }
    }
}
