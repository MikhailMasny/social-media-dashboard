﻿using Microsoft.AspNetCore.Identity;
using SocialMediaDashboard.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaDashboard.Domain.Entities
{
    /// <summary>
    /// Social media account entity.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } // TODO: change it to Account

        /// <summary>
        /// Type.
        /// </summary>
        public AccountType Type { get; set; }

        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// User.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

        /// <summary>
        /// Navigation to Subscription.
        /// </summary>
        public ICollection<Subscription> Subscriptions { get; }
    }
}
