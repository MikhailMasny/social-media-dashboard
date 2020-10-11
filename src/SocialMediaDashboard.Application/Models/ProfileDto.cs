using SocialMediaDashboard.Domain.Enums;
using System;

namespace SocialMediaDashboard.Application.Models
{
    /// <summary>
    /// Profile data transfet object.
    /// </summary>
    public class ProfileDto
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gender.
        /// </summary>
        public GenderType Gender { get; set; }

        /// <summary>
        /// Date of birth.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public byte[] Avatar { get; set; }
    }
}
