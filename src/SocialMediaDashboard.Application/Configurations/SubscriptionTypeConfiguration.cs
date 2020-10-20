using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaDashboard.Application.Constants;
using SocialMediaDashboard.Domain.Entities;
using System;

namespace SocialMediaDashboard.Application.Configurations
{
    /// <summary>
    /// EF Configuration for SubscriptionType entity.
    /// </summary>
    public class SubscriptionTypeConfiguration : IEntityTypeConfiguration<SubscriptionType>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<SubscriptionType> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.SubscriptionTypes, Schema.Counter)
                .HasKey(subscriptionType => subscriptionType.Id);

            builder.HasOne(subscriptionType => subscriptionType.Observation)
                .WithMany(observation => observation.SubscriptionTypes)
                .HasForeignKey(subscriptionType => subscriptionType.ObservationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(subscriptionType => subscriptionType.Platform)
                .WithMany(platform => platform.SubscriptionTypes)
                .HasForeignKey(subscriptionType => subscriptionType.PlatformId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
