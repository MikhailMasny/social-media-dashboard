﻿using SocialMediaDashboard.Common.Models;
using System.Collections.Generic;

namespace SocialMediaDashboard.WebAPI.Contracts.Responses
{
    /// <summary>
    /// Media successful response.
    /// </summary>
    public class MediaSuccessfulResponse
    {
        /// <summary>
        /// Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// All media.
        /// </summary>
        public List<MediaDto> MediaAll { get; } = new List<MediaDto>();
    }
}
