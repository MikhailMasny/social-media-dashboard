using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaDashboard.Application.Constants;
using SocialMediaDashboard.Domain.Entities;
using System;

namespace SocialMediaDashboard.Application.Configurations
{
    /// <summary>
    /// EF Configuration for Statistic entity.
    /// </summary>
    public class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Statistic> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.Statistics, Schema.Counter)
                .HasKey(statistic => statistic.Id);

            builder.HasOne(statistic => statistic.Subscription)
                .WithMany(subscription => subscription.Statistics)
                .HasForeignKey(statistic => statistic.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
