using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaDashboard.Application.Constants;
using SocialMediaDashboard.Domain.Entities;
using System;

namespace SocialMediaDashboard.Application.Configurations
{
    /// <summary>
    /// EF Configuration for Platform entity.
    /// </summary>
    public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Platform> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.Platforms, Schema.Counter)
                .HasKey(platform => platform.Id);

            builder.Property(platform => platform.Description)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthShort);

            builder.Property(platform => platform.Comment)
                .HasMaxLength(SqlConfiguration.SqlMaxLengthLong);
        }
    }
}
