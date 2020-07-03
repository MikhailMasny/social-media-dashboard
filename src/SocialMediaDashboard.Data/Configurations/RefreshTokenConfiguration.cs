using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaDashboard.Domain.Entities;
using System;

namespace SocialMediaDashboard.Data.Configurations
{
    /// <summary>
    /// EF Configuration for RefreshToken model.
    /// </summary>
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable("RefreshTokens")
                .HasKey(r => r.Id);

            builder.Property(r => r.Token)
                .IsRequired();

            builder.Property(r => r.JwtId)
                .IsRequired();

            builder.Property(r => r.CreationDate)
                .IsRequired();

            builder.Property(r => r.ExpiryDate)
                .IsRequired();

            builder.Property(r => r.UserId)
                .IsRequired();
        }
    }
}
