using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

            builder.ToTable("Statistics")
                .HasKey(s => s.Id);

            //builder.HasOne(s => s.Subscription)
            //    .WithMany(s => s.Statistics)
            //    .HasForeignKey(s => s.SubscriptionId);
        }
    }
}
