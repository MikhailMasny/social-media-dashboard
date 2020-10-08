using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaDashboard.Domain.Entities;
using System;

namespace SocialMediaDashboard.Application.Configurations
{
    /// <summary>
    /// EF Configuration for Social media account entity.
    /// </summary>
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable("Accounts")
                .HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired();
        }
    }
}
