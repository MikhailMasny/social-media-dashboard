using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaDashboard.Domain.Entities;
using System;

namespace SocialMediaDashboard.Application.Configurations
{
    /// <summary>
    /// EF Configuration for Social media account entity.
    /// </summary>
    public class CounterConfiguration : IEntityTypeConfiguration<Counter>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Counter> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable("Counters")
                .HasKey(a => a.Id);

            builder.HasOne(todo => todo.User)
                .WithMany(user => user.Counters)
                .HasForeignKey(todo => todo.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
