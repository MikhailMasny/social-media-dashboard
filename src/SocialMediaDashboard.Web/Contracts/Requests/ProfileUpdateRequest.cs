using Microsoft.AspNetCore.Http;
using SocialMediaDashboard.Domain.Enums;
using System;

namespace SocialMediaDashboard.Web.Contracts.Requests
{
    /// <summary>
    /// Profile update request.
    /// </summary>
    public class ProfileUpdateRequest
    {
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
        public IFormFile Avatar { get; set; }
    }
}
