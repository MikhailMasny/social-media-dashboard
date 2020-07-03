﻿using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Interfaces;

namespace SocialMediaDashboard.Common.Models
{
    /// <summary>
    /// Media data transfet object.
    /// </summary>
    public class MediaDto : IHasDbIdentity, IHasUserIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Social media account.
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        public AccountType Type { get; set; }

        /// <inheritdoc/>
        public string UserId { get; set; }
    }
}
