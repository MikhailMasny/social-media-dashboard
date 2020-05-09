using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaDashboard.Domain.Models;

namespace SocialMediaDashboard.Data.Configurations
{
    /// <summary>
    /// EF Configuration for Statistic model.
    /// </summary>
    public class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Statistic> builder)
        {
            builder.ToTable("Statistics")
                .HasKey(s => s.Id);

            builder.HasOne(s => s.Media)
                .WithMany(m => m.Statistics)
                .HasForeignKey(s => s.MediaId);
        }
    }
}
