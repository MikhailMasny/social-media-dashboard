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
        /// Social media account entities.
        /// </summary>
        //public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Subscription entities.
        /// </summary>
        //public DbSet<Subscription> Subscriptions { get; set; }

        /// <summary>
        /// Statistic entities.
        /// </summary>
        public DbSet<Statistic> Statistics { get; set; }

        /// <summary>
        /// Profile entities.
        /// </summary>
        public DbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// RefreshToken entities.
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        /// <summary>
        /// Counters.
        /// </summary>
        public DbSet<Counter> Counters { get; set; }

        public DbSet<Platform> Platforms { get; set; }

        public DbSet<Kind> Kinds { get; set; }

        public DbSet<CounterType> CounterTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            //modelBuilder.ApplyConfiguration(new AccountConfiguration());
            //modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new StatisticConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new CounterConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
