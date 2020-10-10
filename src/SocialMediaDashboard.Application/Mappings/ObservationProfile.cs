using SocialMediaDashboard.Application.Models;
using SocialMediaDashboard.Domain.Entities;

namespace SocialMediaDashboard.Application.Mappings
{
    /// <summary>
    /// AutoMapper profile for Observation.
    /// </summary>
    public class ObservationProfile : AutoMapper.Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ObservationProfile()
        {
            CreateMap<Observation, ObservationDto>().ReverseMap();
        }
    }
}
