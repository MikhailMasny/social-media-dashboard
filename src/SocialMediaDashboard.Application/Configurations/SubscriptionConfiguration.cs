using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaDashboard.Domain.Entities;
using System;

namespace SocialMediaDashboard.Application.Configurations
{
    /// <summary>
    /// EF Configuration for Subscription entity.
    /// </summary>
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable("Subscriptions")
                .HasKey(s => s.Id);

            builder.HasOne(s => s.Account)
                .WithMany(m => m.Subscriptions)
                .HasForeignKey(s => s.AccountId);
        }
    }
}
