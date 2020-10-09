using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaDashboard.Application.Constants;
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

            builder.ToTable(Table.Subscriptions, Schema.Counter)
                .HasKey(subscription => subscription.Id);

            builder.Property(subscription => subscription.UserId)
                .IsRequired();

            builder.Property(subscription => subscription.Name)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthMedium);

            builder.HasOne(subscription => subscription.SubscriptionType)
                .WithMany(subscriptionType => subscriptionType.Subscriptions)
                .HasForeignKey(subscription => subscription.SubscriptionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(subscription => subscription.User)
                .WithMany(user => user.Counters)
                .HasForeignKey(subscription => subscription.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
