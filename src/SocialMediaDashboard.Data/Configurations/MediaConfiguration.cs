using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaDashboard.Domain.Entities;
using System;

namespace SocialMediaDashboard.Data.Configurations
{
    /// <summary>
    /// EF Configuration for Media model.
    /// </summary>
    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable("Medias")
                .HasKey(m => m.Id);

            builder.Property(m => m.AccountName)
                .IsRequired();
        }
    }
}
