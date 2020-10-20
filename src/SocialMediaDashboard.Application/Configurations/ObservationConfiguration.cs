using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaDashboard.Application.Constants;
using SocialMediaDashboard.Domain.Entities;
using System;

namespace SocialMediaDashboard.Application.Configurations
{
    /// <summary>
    /// EF Configuration for Observation entity.
    /// </summary>
    public class ObservationConfiguration : IEntityTypeConfiguration<Observation>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Observation> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.Observations, Schema.Counter)
                .HasKey(observation => observation.Id);

            builder.Property(observation => observation.Description)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthShort);

            builder.Property(observation => observation.Comment)
                .HasMaxLength(SqlConfiguration.SqlMaxLengthLong);
        }
    }
}
