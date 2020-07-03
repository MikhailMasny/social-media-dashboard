using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaDashboard.Domain.Entities;
using System;

namespace SocialMediaDashboard.Data.Configurations
{
    /// <summary>
    /// EF Configuration for Profile model.
    /// </summary>
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable("Profiles")
                .HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
