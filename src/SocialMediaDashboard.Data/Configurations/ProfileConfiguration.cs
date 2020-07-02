using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaDashboard.Domain.Models;
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
                .HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
