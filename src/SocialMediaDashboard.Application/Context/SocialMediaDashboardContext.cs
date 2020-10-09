using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaDashboard.Application.Configurations;
using SocialMediaDashboard.Domain.Entities;
using System;

namespace SocialMediaDashboard.Application.Context
{
    /// <summary>
    /// Database context.
    /// </summary>
    public class SocialMediaDashboardContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">DbContextOptions.</param>
        public SocialMediaDashboardContext(DbContextOptions<SocialMediaDashboardContext> options)
            : base(options) { }

        /// <summary>
        /// Profile entities.
        /// </summary>
        public DbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// RefreshToken entities.
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        /// <summary>
        /// Subscription entities.
        /// </summary>
        public DbSet<Subscription> Subscriptions { get; set; }

        /// <summary>
        /// SubscriptionType entities.
        /// </summary>
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }

        /// <summary>
        /// Platform entities.
        /// </summary>
        public DbSet<Platform> Platforms { get; set; }

        /// <summary>
        /// Observation entities.
        /// </summary>
        public DbSet<Observation> Observations { get; set; }

        /// <summary>
        /// Statistic entities.
        /// </summary>
        public DbSet<Statistic> Statistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PlatformConfiguration());
            modelBuilder.ApplyConfiguration(new ObservationConfiguration());
            modelBuilder.ApplyConfiguration(new StatisticConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
