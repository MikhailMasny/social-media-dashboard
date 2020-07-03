using Microsoft.AspNetCore.Identity;
using SocialMediaDashboard.Common.Enums;
using SocialMediaDashboard.Common.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaDashboard.Domain.Entities
{
    /// <summary>
    /// Media entity.
    /// </summary>
    public class Media : IHasDbIdentity, IHasUserIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Social media account.
        /// </summary>
        public string AccountName { get; set; } // TODO: change it to Account

        /// <summary>
        /// Type.
        /// </summary>
        public AccountType Type { get; set; }

        /// <inheritdoc/>
        public string UserId { get; set; }

        // UNDONE: maybe fix it?
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
