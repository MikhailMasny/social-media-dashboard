using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaDashboard.Application.Constants;
using SocialMediaDashboard.Domain.Entities;
using System;

namespace SocialMediaDashboard.Application.Configurations
{
    /// <summary>
    /// EF Configuration for RefreshToken entity.
    /// </summary>
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.RefreshTokens, Schema.Account)
                .HasKey(refreshToken => refreshToken.Id);

            builder.Property(refreshToken => refreshToken.UserId)
                .IsRequired();

            builder.Property(refreshToken => refreshToken.Token)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthLong);

            builder.Property(refreshToken => refreshToken.JwtId)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthLong);

            builder.HasOne(refreshToken => refreshToken.User)
                .WithMany(user => user.RefreshTokens)
                .HasForeignKey(refreshToken => refreshToken.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
